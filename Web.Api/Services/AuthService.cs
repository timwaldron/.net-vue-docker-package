using Web.Api.Config;
using Web.Api.Models;
using Web.Api.Models.Entities;
using Web.Api.Repositories;
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

        public async Task<AuthTokenDto> Login(AccountDto payload)
        {
            // TODO: Add property sanitization (lowercase?) check MongoDB query
            var filter = Builders<Account>.Filter.Eq(field => field.Email, payload.Email);

            AccountDto account = await _repository.GetByEmail(payload.Email); // Email address is unique
            if (account == null) // Email Address doesn't exist
            {
                return null;
            }

            bool verified = BCrypt.Net.BCrypt.Verify(payload.Password, account.Password);
            if (!verified)
            {
                return null;
            }

            var token = GenerateJwtToken(account);

            return new AuthTokenDto(account, token);
        }

        private string GenerateJwtToken(AccountDto user)
        {
            // generate token that is valid for 120 minutes
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Auth.JwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("id", user.Id.ToString()),
                    new Claim("role", user.Role.ToString("d")),
                    new Claim("email", user.Email),
                }),
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
