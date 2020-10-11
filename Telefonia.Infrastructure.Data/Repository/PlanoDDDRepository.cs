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

        public IEnumerable<PlanoDDD> ListByPlano(PlanoDDD itm)
        {
            var sql = @"SELECT PlanoId, DDDId FROM PlanoDDD WHERE PlanoId = @PlanoId";
            return Context.Query<PlanoDDD>(sql, itm);
        }

        public void Delete(PlanoDDD itm)
        {
            var sql = "DELETE FROM PlanoDDD WHERE PlanoId = @PlanoId AND DDDId = @DDDId";
            Context.ExecuteNonQuery(sql, itm);
        }
    }
}
