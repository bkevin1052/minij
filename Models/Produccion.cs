using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minij.Models
{
    class Produccion
    {
        public int iEstado { get; private set; }
        public string sSimbolo { get; private set; }
        public int iNumeroEstados { get; private set; }

        public Produccion()
        {
            iEstado = default;
            sSimbolo = default;
            iNumeroEstados = default;
        }


        public Produccion(int estado, string simbolo, int numeroEstados)
        {
            this.iEstado = estado;
            this.sSimbolo = simbolo;
            this.iNumeroEstados = numeroEstados;
        }
    }
}
