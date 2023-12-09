using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Models.Entities;

namespace Web.Api.Models
{
    public class AuthTokenDto
    {
        public string Token { get; set; }

        public AuthTokenDto(string token)
        {
            Token = token;
        }
    }
}
