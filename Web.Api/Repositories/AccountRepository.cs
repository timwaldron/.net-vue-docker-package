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

namespace Web.Api.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public override string CollectionName => "accounts";

        public AccountRepository(IAppSettings settings) : base(settings)
        {
        }

        public async Task<AccountDto> Create(AccountDto dto)
        {
            var entity = dto.ToEntity();
            var response = await base.Upsert(entity);

            return response.ToDto();
        }

        // TODO: Don't love this name
        public async Task<List<AccountDto>> FindByQuery(FilterDefinition<Account> filter)
        {
            var entity = await base.FindByFilter(filter);

            return entity?.ToListDto();
        }

        //public async Task<AccountDto> GetById(string id)
        //{
        //    var response = await base.GetById(id);

        //    return response.ToDto();
        //}

        //public async Task<List<AccountDto>> GetUserList()
        //{
        //    // TODO: Refactor this to take a paged settings object (skip/take/order/etc)
        //    var entity = await base.GetAll();
        //    return entity.ToListDto();
        //}

        //public async Task<string> CreateUser(UserRegisterDto dto)
        //{
        //    return await Task.FromResult(string.Empty);
        //}
    }
}
