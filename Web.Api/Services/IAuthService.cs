using Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Services
{
    public interface IAuthService
    {
        Task<AuthTokenDto> Login(AccountDto details);
    }
}
