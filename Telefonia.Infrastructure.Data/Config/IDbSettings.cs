using System;
using System.Collections.Generic;
using System.Text;

namespace Telefonia.Infrastructure.Data.Config
{
    public interface IDbSettings
    {
        string ConnectionString { get; set; }
    }
}
