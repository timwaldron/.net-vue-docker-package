using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Config
{
    public class AppSettings : IAppSettings
    {
        public DatabaseSettings Database { get; set; }
        public AuthSettings Auth { get; set; }
    }
}
