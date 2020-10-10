using System;
using System.Collections.Generic;
using System.Text;
using Telefonia.Context.Context;

namespace Telefonia.Context.Common
{
    public class BasicRepository : IBase
    {
        public IContext Context { get; set; }
        public BasicRepository(IContext context)
        {
            Context = context;
        }
    }
}
