using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Api.Services
{
    public interface IDevService
    {
        void SendMail(string email);
    }
}
