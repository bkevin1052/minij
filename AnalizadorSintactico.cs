using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace minij
{
    class AnalizadorSintactico
    {
        int tokenActual;
        int lookAhead;
        List<Token> tokens;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public AnalizadorSintactico()
        {
            tokenActual = -1;
            lookAhead = 0;
            tokens = new List<Token>();
        }

        /// <summary>
        /// Constructor de la clase para asignar una lista de tokens
        /// </summary>
        /// <param name="listaTokens">Lista de tokens para el analisis sintactico</param>
        public AnalizadorSintactico(List<Token> listaTokens)
        {
            tokenActual = -1;
            lookAhead = 0;
            tokens = listaTokens;
        }


        public void analizar()
        {

        }



        private bool Expr()
        {
            bool value = false;


            
            return value;
        }

        private bool Constant()
        {
            bool value = false;

            if (matchToken("CONSTANTE_ENTERA_DECIMAL") || matchToken("CONSTANTE_ENTERA_HEXADECIMAL"))
                return true;
            if (matchToken("CONSTANTE_DOUBLE"))
                return true;
            if (matchToken("CONSTANTE_BOOLEANA"))
                return true;
            if (matchToken("CADENA"))
                return true;
            if (matchToken("PALABRA_RESERVADA_NULL"))
                return true;

            return value;
        }

        private bool matchToken(string type)
        {
            bool value = false;
            if (tokens[lookAhead].Nombre == type)
            {
                tokenActual = lookAhead;
                lookAhead++;
            }

            return value;
        }
    }
}
