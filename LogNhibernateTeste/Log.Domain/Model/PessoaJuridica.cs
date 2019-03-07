using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Domain.Model
{
    public class PessoaJuridica : Pessoa
    {
        public PessoaJuridica()
        {

        }
        public PessoaJuridica(IServico srv) : base(srv)
        {
        }
        public virtual string Cnpj { get; set; }

    }
}
