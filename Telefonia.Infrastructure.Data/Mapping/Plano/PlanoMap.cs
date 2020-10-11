using Model = Telefonia.Domain;
using Dapper.FluentMap.Dommel.Mapping;

namespace Telefonia.Infrastructure.Data.Mapping.Plano
{
    public class PlanoMap : DommelEntityMap<Model.Plano.Plano>
    {
        public PlanoMap()
        {
            ToTable("Plano");
            Map(x => x.Id).ToColumn("Id").IsKey();
            Map(x => x.Minutos).ToColumn("Minutos");
            Map(x => x.FranquiaInternet).ToColumn("FranquiaInternet");
            Map(x => x.Valor).ToColumn("Valor");
            Map(x => x.StatusRegistro).ToColumn("StatusRegistro");
            Map(x => x.TipoPlanoId).ToColumn("TipoPlanoId");
            Map(x => x.OperadoraId).ToColumn("OperadoraId");
        }
    }
}
