using System;
using System.Collections.Generic;
using System.Text;
using Telefonia.Context.Common;

namespace Telefonia.Infrastructure.Data.Config
{
    public class DbSettings : IDbSettings
    {
        public string ConnectionString { get; set; }
    }
}
