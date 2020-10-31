using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minij.Models
{
    class Validacion: IEquatable<Validacion>
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

        public void setAccion(Validacion validacion)
        {
            sSimbolo = validacion.sSimbolo;
            iEstado = validacion.iEstado;
        }

        public override int GetHashCode() => (sSimbolo, iEstado).GetHashCode();
        public override bool Equals(object obj) => Equals(obj as Validacion);
        public override string ToString() => $"Simbolo:{this.sSimbolo} , Estado:{this.iEstado}";

        public bool Equals(Validacion other)
        {
            if (other is null)
                return false;

            return (this.sSimbolo == other.sSimbolo && this.iEstado == other.iEstado);
        }
    }
}
