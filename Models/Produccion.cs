using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minij.Models
{
    class Produccion: IEquatable<Produccion>
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


        public override int GetHashCode() => (sSimbolo, iEstado, iNumeroEstados).GetHashCode();
        public override bool Equals(object obj) => Equals(obj as Produccion);
        public override string ToString() => $"Estado:{this.iEstado} , Simbolo:{this.sSimbolo} , No. elementos:{this.iNumeroEstados}";

        public bool Equals(Produccion other)
        {
            if (other is null)
                return false;

            return (this.sSimbolo == other.sSimbolo && this.iEstado == other.iEstado && this.iNumeroEstados == other.iNumeroEstados);
        }
    }
}
