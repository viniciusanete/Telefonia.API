using System;
using System.Collections.Generic;
using System.Text;
using Telefonia.Context.Context;

namespace Telefonia.Domain.PlanoDDD
{
    public interface IPlanoDDDRepository
    {
        IContext Context { get; }
        PlanoDDD Insert(PlanoDDD itm);
    }
}
