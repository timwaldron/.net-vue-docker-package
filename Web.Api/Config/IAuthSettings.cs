using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Config
{
    public interface IAuthSettings
    {
        public string Secret { get; set; }
    }
}
