using System.Threading.Tasks;
using Web.Api.Models;
using Web.Api.Repositories;
using BCrypt.Net;

namespace Web.Api.Services
{
    public class AccountService : IAccountService
    {
        IAccountRepository _repository;

        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Create(AccountDto account)
        {
            account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);

            var response = await _repository.Create(account);

            return (response != null);
        }

        public async Task<AccountDto> GetByEmail(string email)
        {
            var response = await _repository.GetByEmail(email);

            return response;
        }
    }
}
