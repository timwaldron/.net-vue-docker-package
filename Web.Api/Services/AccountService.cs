using System.Threading.Tasks;
using Web.Api.Models;
using Web.Api.Repositories;

namespace Web.Api.Services
{
    public class AccountService : IAccountService
    {
        IAccountRepository _repository;

        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<AccountDto> Create(AccountDto account)
        {
            var response = await _repository.Create(account);

            return response;
        }
    }
}
