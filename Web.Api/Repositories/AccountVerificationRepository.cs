
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
    public class AccountVerificationRepository : RepositoryBase<AccountVerification>, IAccountVerificationRepository
    {
        public override string CollectionName => "accountverifications";

        public AccountVerificationRepository(IAppSettings settings) : base(settings) { }

        public async Task<AccountVerificationDto> Save(AccountVerificationDto dto)
        {
            var entity = dto.ToEntity();
            var response = await base.Upsert(entity);

            return response.ToDto();
        }

        public async Task<AccountVerificationDto> GetByEmail(string email)
        {
            var response = await FindByFilter(Builders<AccountVerification>.Filter.Eq("Email", email));
            var entity = response?.FirstOrDefault();

            return entity?.ToDto();
        }

        public async Task<bool> Delete(string id)
        {
            return await base.DeleteById(id);
        }
    }
}
