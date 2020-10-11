using System;
using System.Collections.Generic;
using System.Text;

namespace Telefonia.Domain.Plano
{
    public interface IPlanoRepository : Telefonia.Context.Common.IRegular<Plano, int>
    {
        IEnumerable<Plano> List(Filter filter);
    }
}
