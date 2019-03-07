using FluentNHibernate.Mapping;

namespace Log.Domain.Mapping.Mapping
{
    public class LogMap : ClassMap<Log.Domain.Model.Log>
    {
        public LogMap()
        {
            Id(x => x.Id);
            Map(x => x.Usuario);
            Map(x => x.Data);
            Map(x => x.Entidade);            
        }
    }
}
