using FluentNHibernate.Mapping;
using Log.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Domain.Mapping
{
    public class PessoaMap : ClassMap<Pessoa>
    {
        public PessoaMap()
        {
            Id(x => x.Id);
            Map(x => x.Nome);
        }
    }
}
