using Web.Api.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Api.Services.Interfaces;

namespace Web.Api.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAppSettings _settings;

        public AuthMiddleware(RequestDelegate next, IAppSettings settings)
        {
            _next = next;
            _settings = settings;
        }

        public async Task Invoke(HttpContext context, IAccountService _accountService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();

            if (token != null)
                await AttachUserToContext(context, _accountService, token);

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, IAccountService _accountService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_settings.Auth.Secret);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var email = (validatedToken as JwtSecurityToken).Claims.First(c => c.Type == "email").Value;
                context.Items["Account"] = await _accountService.GetByEmail(email);
            }
            catch
            {
                // TODO: Log something.
            }
        }
    }
}
