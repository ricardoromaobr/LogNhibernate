using Log.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Domain.Model
{
    public class ColunaAlterada : Entidade
    {
        public virtual string Coluna { get; set; }
        public virtual string ValorAtual { get; set; }
        public virtual string ValorAnterior { get; set; }
        public virtual Log Log { get; set; }
    }
}
