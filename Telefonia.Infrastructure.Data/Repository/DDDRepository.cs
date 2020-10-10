using System;
using System.Collections.Generic;
using System.Text;
using Telefonia.Context.Common;
using Telefonia.Context.Context;
using Telefonia.Domain.DDD;

namespace Telefonia.Infrastructure.Data.Repository
{
    public class DDDRepository : RegularRepository<DDD, int>, IDDDRepository
    {
        public DDDRepository(IContext context) : base(context)
        { }

        public DDD GetByDDD(DDD frm)
        {
            var sql = @"SELECT Id, Codigo, Descricao FROM DDD WHERE Codigo = @Codigo";
            
            return Context.QueryFirstOrDefault<DDD>(sql, frm);
        }

        public IEnumerable<DDD> ListByPlano(Filter filter)
        {
            var sql = @"SELECT Id, Codigo, Descricao FROM DDD D INNER JOIN PlanoDDD PD ON D.Id = PD.DDDId WHERE PD.PlanoId = @PlanoId";

            return Context.Query<DDD>(sql, filter);
        }
    }
}
