using Model = Telefonia.Domain;
using Dapper.FluentMap.Dommel.Mapping;

namespace Telefonia.Infrastructure.Data.Mapping.Operadora
{
    public class OperadoraMap : DommelEntityMap<Model.Operadora.Operadora>
    {
        public OperadoraMap()
        {
            ToTable("Operadora");
            Map(x => x.Id).ToColumn("Id").IsKey();
            Map(x => x.Nome).ToColumn("Nome");
        }
    }
}
