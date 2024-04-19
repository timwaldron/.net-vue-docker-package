namespace Web.Api.Config
{
    public class MailSettings : IMailSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SenderEmail { get; set; }
        public bool EnableSsl { get; set; }
        public bool BlockMailSend { get; set; }
    }
}
