using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Api.Models;
using Web.Api.Services.Interfaces;

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
        public async Task<ActionResult<ServiceResult<AuthTokenDto>>> Login([FromBody] AccountDto account)
        {
            var response = await _authService.Login(account);

            // Not sure how I feel about this...
            if (response.Status == ServiceResultStatus.Success.ToString())
            {
                return Ok(response);
            }

            return Unauthorized(response);
        }
    }
}
