using System.Net;
using System;
using System.Threading.Tasks;
using Web.Api.Config;
using System.Net.Http;
using System.Net.Mail;

namespace Web.Api.Services
{
    public class MailService : IMailService
    {
        private readonly SmtpClient smtpClient;
        private readonly string senderEmail;

        public MailService(IAppSettings settings)
        {
            senderEmail = settings.Mail.SenderEmail;

            smtpClient = new SmtpClient(settings.Mail.SmtpServer);
            smtpClient.Port = settings.Mail.SmtpPort;
            smtpClient.Credentials = new NetworkCredential(settings.Mail.Username, settings.Mail.Password);
            smtpClient.EnableSsl = settings.Mail.EnableSsl;
        }

        public void SendMail(MailMessage mailMessager)
        {
            mailMessager.From = new MailAddress(senderEmail);
            smtpClient.Send(mailMessager);
        }
    }
}
