using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Api.Models;
using Web.Api.Services.Interfaces;

namespace Web.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("create")]
        public async Task<OperationResult> Create([FromBody] AccountDto account)
        {
            var result = await _accountService.Create(account);

            if (result.Outcome == OperationOutcome.Failure.ToString())
            {
                Response.StatusCode = 400;
            }

            return result;
        }

        [HttpGet("verify")]
        public async Task<OperationResult> Verify(string email, string code)
        {
            var result = await _accountService.Verify(email, code);

            if (result.Outcome != OperationOutcome.Success.ToString())
            {
                Response.StatusCode = 400;
            }

            return result;
        }
    }
}
