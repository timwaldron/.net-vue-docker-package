using System.Net.Mail;
using System.Threading.Tasks;

namespace Web.Api.Services
{
    public interface IMailService
    {
        void SendMail(MailMessage mailMessager);
    }
}
