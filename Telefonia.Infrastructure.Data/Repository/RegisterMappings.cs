using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;

namespace Telefonia.Infrastructure.Data.Repository
{
    public class RegisterMappings
    {
        public static void Register()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new Mapping.DDD.DDDMap());
                config.AddMap(new Mapping.Operadora.OperadoraMap());
                config.AddMap(new Mapping.Plano.PlanoMap());
                config.AddMap(new Mapping.PlanoDDD.PlanoDDDMap());
                config.AddMap(new Mapping.TipoPlano.TipoPlanoMap());
                config.ForDommel();
            });
        }
    }
}
