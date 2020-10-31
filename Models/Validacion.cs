using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minij.Models
{
    class Validacion
    {
        public string sSimbolo { get; private set; }
        public int iEstado { get; private set; }

        public Validacion()
        {
            sSimbolo = default;
            iEstado = default;
        }


        public Validacion(string simbolo, int estado)
        {
            this.sSimbolo = simbolo;
            this.iEstado = estado;
        }
    }
}
