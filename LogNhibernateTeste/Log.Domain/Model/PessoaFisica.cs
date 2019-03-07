using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Domain.Model
{
    public class PessoaFisica : Pessoa
    {
        public PessoaFisica() : base()
        {

        }
        public PessoaFisica(IServico srv) : base (srv)
        {

        }
        public virtual string Cpf { get; set; }

    }
}
