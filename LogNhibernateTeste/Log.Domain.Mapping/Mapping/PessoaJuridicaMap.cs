using FluentNHibernate.Mapping;
using Log.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Domain.Mapping.Mapping
{
    public class PessoaJuridicaMap : SubclassMap<PessoaJuridica>
    {
        public PessoaJuridicaMap()
        {
            Map(x => x.Cnpj)                
                .Unique();
        }
    }
}
