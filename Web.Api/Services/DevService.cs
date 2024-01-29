using System.Net.Mail;
using System.Threading.Tasks;

namespace Web.Api.Services
{
    public class DevService : IDevService
    {
        private readonly IMailService _mailService;

        public DevService(IMailService mailService)
        {
            _mailService = mailService;
        }

        public void SendMail(string email)
        {
            var mail = new MailMessage();
            mail.To.Add(email);
            mail.Subject = "Hello from Dev Server";
            mail.Body = "This is a test email sent using SMTP in C#.\n\nTest separate lines";

            _mailService.SendMail(mail);
        }
    }
}
