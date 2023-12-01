using Web.Api.Models;
using Web.Api.Models.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Repositories
{
    public interface IAccountRepository
    {
        Task<AccountDto> Create(AccountDto dto);
        Task<List<AccountDto>> FindByQuery(FilterDefinition<Account> filter);
    }
}
