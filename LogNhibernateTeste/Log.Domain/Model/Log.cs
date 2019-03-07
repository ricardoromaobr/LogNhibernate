using Log.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Domain.Model
{
    public class Log : Entidade
    {
        public virtual string Entidade { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual string Usuario { get; set; }
    }
}
