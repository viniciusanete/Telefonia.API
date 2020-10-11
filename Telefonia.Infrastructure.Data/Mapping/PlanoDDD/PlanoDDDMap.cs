using Model = Telefonia.Domain;
using Dapper.FluentMap.Dommel.Mapping;

namespace Telefonia.Infrastructure.Data.Mapping.PlanoDDD
{
    public class PlanoDDDMap : DommelEntityMap<Model.PlanoDDD.PlanoDDD>
    {
        public PlanoDDDMap()
        {
            ToTable("PlanoDDD");
            Map(x => x.PlanoId).ToColumn("PlanoId").IsKey();
            Map(x => x.DDDId).ToColumn("DDDId").IsKey();
        }
    }
}
