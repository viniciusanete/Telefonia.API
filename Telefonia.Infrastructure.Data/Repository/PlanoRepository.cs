using System;
using System.Collections.Generic;
using System.Text;
using Telefonia.Context.Common;
using Telefonia.Context.Context;
using Telefonia.Domain.Plano;

namespace Telefonia.Infrastructure.Data.Repository
{
    public class PlanoRepository : RegularRepository<Plano, int>, IPlanoRepository
    {
        public PlanoRepository(IContext context) : base(context)
        { }
    }
}
