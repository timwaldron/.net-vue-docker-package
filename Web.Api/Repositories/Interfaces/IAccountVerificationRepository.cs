using Web.Api.Models;
using Web.Api.Models.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Repositories.Interfaces
{
    public interface IAccountVerificationRepository
    {
        Task<AccountVerificationDto> Save(AccountVerificationDto dto);
        Task<AccountVerificationDto> GetByEmail(string email);
        Task<bool> Delete(string id);
    }
}
