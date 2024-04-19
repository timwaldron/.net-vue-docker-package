using System.Net;
using System;
using System.Threading.Tasks;
using Web.Api.Config;
using System.Net.Http;
using System.Net.Mail;
using System.Diagnostics;
using Web.Api.Services.Interfaces;

namespace Web.Api.Services
{
    public class MailService : IMailService
    {
        private readonly SmtpClient smtpClient;
        private readonly string senderEmail;
        private readonly bool blockMailSend;

        public MailService(IAppSettings settings)
        {
            senderEmail = settings.Mail.SenderEmail;

            smtpClient = new SmtpClient(settings.Mail.SmtpServer);
            smtpClient.Port = settings.Mail.SmtpPort;
            smtpClient.Credentials = new NetworkCredential(settings.Mail.Username, settings.Mail.Password);
            smtpClient.EnableSsl = settings.Mail.EnableSsl;

            blockMailSend = settings.Mail.BlockMailSend;
        }

        public async Task SendMail(MailMessage mailMessager)
        {
            mailMessager.From = new MailAddress(senderEmail);

            // TODO: Not sure if I'm happy with this, fine for now
            if (blockMailSend)
            {
                Debug.WriteLine("blockMailSend set to true, ignoring...");
            }
            else
            {
                await smtpClient.SendMailAsync(mailMessager);
            }
        }
    }
}
