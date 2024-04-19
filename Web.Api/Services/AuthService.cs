using Web.Api.Config;
using Web.Api.Models;
using Web.Api.Models.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.Api.Services.Interfaces;
using Web.Api.Repositories.Interfaces;

namespace Web.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAppSettings _settings;
        private readonly IAccountRepository _repository;
        
        public AuthService(IAppSettings settings, IAccountRepository repository)
        {
            _settings = settings;
            _repository = repository;
        }

        public async Task<ServiceResult<AuthTokenDto>> Login(AccountDto payload)
        {
            // TODO: Add property sanitization (lowercase?) check MongoDB query
            var filter = Builders<Account>.Filter.Eq(field => field.Email, payload.Email);

            AccountDto account = await _repository.GetByEmail(payload.Email); // Email address is unique
            if (account == null) // Email Address doesn't exist
            {
                return new ServiceResult<AuthTokenDto>(null, ServiceResultStatus.Failure, "Invalid email or password");
            }

            bool verified = BCrypt.Net.BCrypt.Verify(payload.Password, account.Password);
            if (verified == false) // Passwords don't match
            {
                return new ServiceResult<AuthTokenDto>(null, ServiceResultStatus.Failure, "Invalid email or password");
            }

            var authTokenDto = new AuthTokenDto(GenerateJwtToken(account));

            return new ServiceResult<AuthTokenDto>(authTokenDto, ServiceResultStatus.Success);
        }

        private string GenerateJwtToken(AccountDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Auth.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("id", user.Id.ToString()),
                    new Claim("role", user.Role.ToString("d")),
                    new Claim("email", user.Email),
                    new Claim("verified", user.Verified ? "Y" : "N"),
                }),
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
