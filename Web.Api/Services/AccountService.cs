using System.Threading.Tasks;
using Web.Api.Models;
using BCrypt.Net;
using System.Net.Mail;
using System;
using Web.Api.Config;
using Web.Api.Services.Interfaces;
using Web.Api.Repositories.Interfaces;

namespace Web.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly string hostDomain;

        private readonly IMailService _mailService;
        private readonly IAccountRepository _repository;
        private readonly IAccountVerificationRepository _accountVerificationRepository;

        public AccountService(
            IAppSettings settings,
            IMailService mailService,
            IAccountRepository repository,
            IAccountVerificationRepository accountVerificationRepository)
        {
            hostDomain = settings.HostDomain;

            _mailService = mailService;
            _repository = repository;
            _accountVerificationRepository = accountVerificationRepository;
        }

        public async Task<AccountDto> GetByEmail(string email)
        {
            var response = await _repository.GetByEmail(email);

            return response;
        }

        // TODO: Refactor out some logic in this function to smaller more testable functions
        public async Task<OperationResult> Create(AccountDto account)
        {
            // Check for existing account
            var existingAccount = await _repository.GetByEmail(account.Email);
            if (existingAccount != null)
            {
                return new OperationResult(OperationOutcome.Failure, "Email address is already registered to an account");
            }

            // Hash the password, set some default values, save the account
            account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            account.Verified = false;
            account.Locked = false;

            var savedAccount = await _repository.Save(account);
            if (savedAccount == null)
            {
                // Should be a return a 500 and be logged
                return new OperationResult(OperationOutcome.Failure, "Failed to create account");
            }

            // Create verification
            var savedVerification = await CreateAccountVerification(savedAccount);
            if (savedVerification == null)
            {
                // Should be a return a 500 and be logged
                return new OperationResult(OperationOutcome.Failure, "Failed to create account verification");
            }

            // Create email and send it to the email provided
            await SendVerificationEmail(account, savedVerification);
            
            return new OperationResult(OperationOutcome.Success, "Account created successfully");
        }

        public async Task<OperationResult> Verify(string email, string code)
        {
            var account = await GetByEmail(email);
            if (account == null)
            {
                return new OperationResult(OperationOutcome.Failure, "Failed to verify account");
            }

            // Already verified, can't re-verify
            if (account.Verified)
            {
                return new OperationResult(OperationOutcome.Failure, "Failed to verify account");
            }

            var verification = await _accountVerificationRepository.GetByEmail(email);
            if (verification == null)
            {
                var newVerification = await CreateAccountVerification(account);
                await SendVerificationEmail(account, newVerification);

                return new OperationResult(OperationOutcome.Warning, "Verification missing, please check email for new code");
            }

            if (account.Locked)
            {
                return new OperationResult(OperationOutcome.Failure, "Account locked, please contact support");
            }

            if (verification.AuthCode != code)
            {
                verification.Attempts += 1;
                await _accountVerificationRepository.Save(verification);

                if (verification.Attempts > 5)
                {
                    account.Locked = true;
                    await _repository.Save(account);

                    return new OperationResult(OperationOutcome.Failure, "Account locked, please contact support");
                }

                return new OperationResult(OperationOutcome.Failure, "Failed to verify account");
            }

            if (verification.Expiry < DateTime.UtcNow)
            {
                verification.AuthCode = new Random().Next(100000, 999999).ToString();
                verification.Expiry = DateTime.UtcNow.AddHours(2);
                verification.Attempts += 1;
                await _accountVerificationRepository.Save(verification);

                if (verification.Attempts > 5)
                {
                    account.Locked = true;
                    await _repository.Save(account);

                    return new OperationResult(OperationOutcome.Failure, "Account locked, please contact support");
                }

                await SendVerificationEmail(account, verification);

                return new OperationResult(OperationOutcome.Failure, "Verification code expired, check email for a new code");
            }

            account.Verified = true;
            await _repository.Save(account);
            await _accountVerificationRepository.Delete(verification.Id);

            return new OperationResult(OperationOutcome.Success, "Account successfully verified");
        }

        private async Task SendVerificationEmail(AccountDto account, AccountVerificationDto verification)
        {
            var mail = new MailMessage();

            mail.To.Add(account.Email);
            mail.Subject = "Boilerplate - Verify Email Account";
            mail.IsBodyHtml = true;

            // We only want to send thank you if they haven't attempted to verify yet
            var thankYouMsg = "";
            if (verification.Attempts == 0)
            {
                thankYouMsg = "Thank you for registering!<br><br>";
            }


            mail.Body = thankYouMsg +
                $"Your verification code is: {verification.AuthCode}<br><br>" +
                $"You can also verify by following this link: <a href=\"{hostDomain}/verify?email={account.Email}&code={verification.AuthCode}\">{hostDomain}/verify?email={account.Email}&code={verification.AuthCode}</a>";

            await _mailService.SendMail(mail);
        }

        private async Task<AccountVerificationDto> CreateAccountVerification(AccountDto account)
        {
            // Create the account verification
            var verification = new AccountVerificationDto()
            {
                Email = account.Email,
                Attempts = 0,
                AuthCode = new Random().Next(100000, 999999).ToString(),
                Expiry = DateTime.UtcNow.AddHours(2),
            };

            return await _accountVerificationRepository.Save(verification);
        }
    }
}
