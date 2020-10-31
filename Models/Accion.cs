using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minij.Models
{
    class Accion
    {
        public string sRegla { get; private set; }

        public int iEstado { get; private set; }

        public Accion()
        {
            sRegla = default;
            iEstado = default;
        }

        public Accion(string regla, int estado)
        {
            this.sRegla = regla;
            this.iEstado = estado;
        }
    }
}
