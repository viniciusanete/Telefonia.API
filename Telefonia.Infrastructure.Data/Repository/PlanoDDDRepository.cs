using System;
using System.Collections.Generic;
using System.Text;
using Telefonia.Context.Context;
using Telefonia.Domain.PlanoDDD;

namespace Telefonia.Infrastructure.Data.Repository
{
    public class PlanoDDDRepository : IPlanoDDDRepository
    {
        public IContext Context { get; set; }
        public PlanoDDDRepository(IContext context)
        {
            Context = context;
        }

        public PlanoDDD Insert(PlanoDDD itm)
        {
            var sql = @"INSERT INTO PlanoDDD (PlanoId, DDDId) VALUES (@PlanoId, @DDDId)";

            Context.ExecuteNonQuery(sql, itm);

            return itm;
        }
    }
}
