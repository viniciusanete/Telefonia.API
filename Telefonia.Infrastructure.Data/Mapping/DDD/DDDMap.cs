using Model = Telefonia.Domain;
using Dapper.FluentMap.Dommel.Mapping;

namespace Telefonia.Infrastructure.Data.Mapping.DDD
{
    public class DDDMap : DommelEntityMap<Model.DDD.DDD>
    {
        public DDDMap()
        {
            ToTable("DDD");
            Map(x => x.Id).ToColumn("Id").IsKey();
            Map(x => x.Codigo).ToColumn("Codigo");
            Map(x => x.Descricao).ToColumn("Descricao");
        }
    }
}
