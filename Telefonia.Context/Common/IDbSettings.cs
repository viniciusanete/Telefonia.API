using System;
using System.Collections.Generic;
using System.Text;

namespace Telefonia.Context.Common
{
    public interface IDbSettings
    {
        string ConnectionString { get; set; }
    }
}
