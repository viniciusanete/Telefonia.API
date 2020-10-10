using System;
using System.Collections.Generic;
using System.Text;

namespace Telefonia.Infrastructure.Data.Config
{
    public class DbSettings : IDbSettings
    {
        public string ConnectionString { get; set; }
    }
}
