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

        public IEnumerable<Plano> List(Filter filter)
        {
            var sql = @"SELECT
                            P.Id,
                            P.Minutos,
                            P.FranquiaInternet,
                            P.Valor,
                            P.StatusRegistro,
                            P.TipoPlanoId,
                            P.OperadoraId
                        FROM Plano P
                            INNER JOIN PlanoDDD PD ON P.Id = PD.PlanoId
                        WHERE 
                            PD.DDDId = @DDD AND
                            (@TipoPlanoId IS NULL OR P.TipoPlanoId = @TipoPlanoId) AND
                            (@OperadoraId IS NULL OR P.OperadoraId = @OperadoraId) AND
                            (@PlanoId IS NULL OR P.Id = @PlanoId) AND P.StatusRegistro = 1         
                      ";

            return Context.Query<List>(sql, filter);
        }
    }
}
