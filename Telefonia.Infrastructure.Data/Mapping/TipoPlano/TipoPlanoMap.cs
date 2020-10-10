using Model = Telefonia.Domain;
using Dapper.FluentMap.Dommel.Mapping;

namespace Telefonia.Infrastructure.Data.Mapping.TipoPlano
{
    public class TipoPlanoMap : DommelEntityMap<Model.TipoPlano.TipoPlano>
    {
        public TipoPlanoMap()
        {
            ToTable("TipoPlano");
            Map(x => x.Id).ToColumn("Id").IsKey();
            Map(x => x.Nome).ToColumn("Nome");
        }
    }
}
