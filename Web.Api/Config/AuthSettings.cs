using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Config
{
    public class AuthSettings : IAuthSettings
    {
        public string Secret { get; set; }
    }
}
