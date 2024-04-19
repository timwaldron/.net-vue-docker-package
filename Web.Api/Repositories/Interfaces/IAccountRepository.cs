using Web.Api.Models;
using Web.Api.Models.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<AccountDto> Save(AccountDto dto);
        Task<AccountDto> GetByEmail(string email);
    }
}
