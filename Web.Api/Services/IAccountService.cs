using System.Threading.Tasks;
using Web.Api.Models;

namespace Web.Api.Services
{
    public interface IAccountService
    {
        Task<AccountDto> Create(AccountDto account);
    }
}
