using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minij.Models
{
    class Accion: IEquatable<Accion>
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

        public void setAccion(Accion accion)
        {
            sRegla = accion.sRegla;
            iEstado = accion.iEstado;
        }


        public override int GetHashCode() => (sRegla, iEstado).GetHashCode();
        public override bool Equals(object obj) => Equals(obj as Accion);
        public override string ToString() => $"Regla:{this.sRegla} , Estado:{this.iEstado}";

        public bool Equals(Accion other)
        {
            if (other is null)
                return false;

            return (this.sRegla == other.sRegla && this.iEstado == other.iEstado);
        }
    }
}
