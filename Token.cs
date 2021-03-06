﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minij
{
    public class Token
    {

        public string Nombre { get; set; }
        public string Lexema { get; set; }

        public int Index { get; private set; }
        public int Linea { get; private set; }
        public int Columna { get; private set; }
        public int Lenght { get { return Lexema.Length; } }


        public Token(string nombre, string lexema, int index, int linea, int columna)
        {
            Nombre = nombre;
            Lexema = lexema;
            Index = index;
            Linea = linea;
            Columna = columna;
        }
    }
}
