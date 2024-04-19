using System;

namespace Web.Api.Models
{
    public class AccountVerificationDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string AuthCode { get; set; }
        public int Attempts { get; set; }
        public DateTime Expiry { get; set; }
    }
}
