using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Web.Api.Models;
using Web.Api.Services;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class DevController : ControllerBase
    {
        private readonly IDevService _devService;

        public DevController(IDevService devService)
        {
            _devService = devService;
        }

        [HttpGet("test-mail")]
        public ActionResult TestMailer(string email)
        {
            try
            {
                _devService.SendMail(email);
            }
            catch
            {
                return UnprocessableEntity();
            }

            return Ok();
        }

        [HttpGet("logged-in")]
        [RoleGuard(Role.User)]
        public ActionResult CheckLoggedIn()
        {
            return Ok();
        }

        [HttpGet("is-admin")]
        [RoleGuard(Role.Admin)]
        public ActionResult CheckAdminLogin()
        {
            return Ok();
        }

        [HttpGet("json")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = summaries[rng.Next(summaries.Length)]
            })
            .ToArray();
        }
    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public string Summary { get; set; }
    }
}
