using Web.Api.Config;
using Web.Api.Mappers;
using Web.Api.Models;
using Web.Api.Models.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Repositories.Interfaces;

namespace Web.Api.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public override string CollectionName => "accounts";

        public AccountRepository(IAppSettings settings) : base(settings) { }

        public async Task<AccountDto> Save(AccountDto dto)
        {
            var entity = dto.ToEntity();
            var response = await base.Upsert(entity);

            return response.ToDto();
        }

        public async Task<AccountDto> GetByEmail(string email)
        {
            var existingAccount = await FindByFilter(Builders<Account>.Filter.Eq("Email", email));
            var entity = existingAccount?.FirstOrDefault();

            return entity?.ToDto();
        }
    }
}
