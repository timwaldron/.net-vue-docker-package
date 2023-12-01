using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Models.Entities;

namespace Web.Api.Models
{
    public class AuthTokenDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }

        public AuthTokenDto(AccountDto account, string token)
        {
            Id = account.Id;
            Email = account.Email;
            Role = account.Role;
            Token = token;
        }
    }
}
