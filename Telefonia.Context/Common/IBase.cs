using System;
using System.Collections.Generic;
using System.Text;
using Telefonia.Context.Context;

namespace Telefonia.Context.Common
{
    public interface IBase
    {
        IContext Context { get; set; }
    }
}
