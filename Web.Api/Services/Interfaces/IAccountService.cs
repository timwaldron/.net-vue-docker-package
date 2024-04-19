using System.Threading.Tasks;
using Web.Api.Models;

namespace Web.Api.Services.Interfaces
{
    public interface IAccountService
    {
        Task<OperationResult> Create(AccountDto account);
        Task<AccountDto> GetByEmail(string email);
        Task<OperationResult> Verify(string email, string code);
    }
}
