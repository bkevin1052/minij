using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minij
{
    public class Lectura
    {
        private StreamReader lecturaArchivo;
        private Stack<Error> pilaErrores;
        private string linea;
        private string ruta;

        public Lectura(string ruta) {
            this.ruta = ruta;
        }

        public void LeerComentarios(string ruta) { }
        
        public void LeerEspacios(string ruta) { }
        
        public void LeerIdentificadores(string ruta) { }

        public void LeerConstantes(string ruta) { }

        public void LeerOperadores(string ruta) { }

        public void LeerCaracteres(string ruta) { }
    }
}
