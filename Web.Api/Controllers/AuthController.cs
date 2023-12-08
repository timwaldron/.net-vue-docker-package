using Web.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Api.Models;

namespace Web.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthTokenDto>> Login([FromBody] AccountDto account)
        {
            var response = await _authService.Login(account);

            // TODO: Address this, shouldn't just assume 'null' is incorrect password
            if (response == null)
            {
                return Unauthorized("Incorrect password");
            }

            return Ok(response);
        }
    }
}
