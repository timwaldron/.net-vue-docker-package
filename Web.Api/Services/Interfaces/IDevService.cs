using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Api.Services.Interfaces
{
    public interface IDevService
    {
        Task SendMail(string email);
    }
}
