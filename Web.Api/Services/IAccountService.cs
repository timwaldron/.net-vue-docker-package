using System.Threading.Tasks;
using Web.Api.Models;

namespace Web.Api.Services
{
    public interface IAccountService
    {
        Task<bool> Create(AccountDto account);
        Task<AccountDto> GetByEmail(string email);
    }
}
