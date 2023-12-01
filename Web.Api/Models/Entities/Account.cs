namespace Web.Api.Models.Entities
{
    public class Account
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
