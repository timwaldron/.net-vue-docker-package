using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Config
{
    public interface IDatabaseSettings
    {
        public string ConnectionUrl { get; set; }
        public string Name { get; set; }
        public int Port { get; set; }
        public string AdminUser { get; set; }
        public string AdminDb { get; set; }
        public string AdminPass { get; set; }
    }
}
