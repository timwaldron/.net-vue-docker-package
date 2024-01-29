using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Config
{
    public class AppSettings : IAppSettings
    {
        public string HostDomain { get; set; }
        public DatabaseSettings Database { get; set; }
        public AuthSettings Auth { get; set; }
        public MailSettings Mail { get; set; }
    }
}
