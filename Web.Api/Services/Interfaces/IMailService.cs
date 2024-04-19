using System.Net.Mail;
using System.Threading.Tasks;

namespace Web.Api.Services.Interfaces
{
    public interface IMailService
    {
        Task SendMail(MailMessage mailMessager);
    }
}
