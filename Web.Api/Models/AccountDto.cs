namespace Web.Api.Models
{
    public class AccountDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Verified { get; set; }
        public bool Locked { get; set; }
        public Role Role { get; set; }
    }

    public enum Role
    {
        User = 0,
        Admin = 100,
    }
}
