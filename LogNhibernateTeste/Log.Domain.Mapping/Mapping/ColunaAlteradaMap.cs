using FluentNHibernate.Mapping;
using Log.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Domain.Mapping.Mapping
{
    public class ColunaAlteradaMap : ClassMap<ColunaAlterada>
    {
        public ColunaAlteradaMap()
        {
            Id(x => x.Id);
            Map(x => x.Coluna);
            Map(x => x.ValorAnterior);
            Map(x => x.ValorAtual);

            References(x => x.Log);
        }
    }
}
