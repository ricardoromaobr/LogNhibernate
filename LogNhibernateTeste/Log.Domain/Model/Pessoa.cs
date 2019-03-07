using Log.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Domain.Model
{
    public class Pessoa : Entidade
    {
        private IServico srv;


        public Pessoa()
        {
        }
        public Pessoa(IServico srv)
        {
            this.srv = srv;
        }
        public virtual string Nome { get; set; }
        public virtual int Do ()
        {
            return srv.Do();
        }

    }
}
