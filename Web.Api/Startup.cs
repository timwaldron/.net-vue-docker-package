using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Config;
using Web.Api.Middleware;
using Web.Api.Repositories;
using Web.Api.Repositories.Interfaces;
using Web.Api.Services;
using Web.Api.Services.Interfaces;

namespace Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Settings for application
            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));
            services.AddSingleton<IAppSettings>(sp => sp.GetRequiredService<IOptions<AppSettings>>().Value);

            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IDevService, DevService>();
            services.AddScoped<IMailService, MailService>();

            // Repositories
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountVerificationRepository, AccountVerificationRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();

            app.UseMiddleware<AuthMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
