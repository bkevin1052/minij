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
        List<Token> tokensErroneos;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public AnalizadorSintactico()
        {
            tokenActual = -1;
            lookAhead = 0;
            tokens = new List<Token>();
            tokensErroneos = new List<Token>();
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
            tokensErroneos = new List<Token>();
        }


        public void analizar()
        {

        }

        private bool Variable()
        {
            bool value = false;

            return value;
        }

        private bool FunctionDecl()
        {
            bool value = false;

            return value;
        }

        private bool Function()
        {
            bool value = false;

            if (matchToken("IDENTIFICADOR"))
                if (matchToken("PARENTESIS_ABRE"))
                    if (Formals())
                        if (matchToken("PARENTESIS_CIERRA"))
                            if (Function_hijo())
                                return true;

            return value;
        }

        private bool Function_hijo()
        {
            bool value = true;

            if (Stmt())
                if (Function_hijo())
                    return true;

            return value;
        }

        private bool Formals()
        {
            bool value = true;

            if (Variable())
                if (Formals_hijo())
                        return true;

            return value;
        }

        private bool Formals_hijo()
        {
            bool value = true;

            if (matchToken("DELIMITADOR_COMA"))
                if (Variable())
                    if (Formals_hijo())
                        return true;

            return value;
        }

        private bool Stmt()
        {
            bool value = false;

            if (ForStmt())
                return true;
            if (ReturnStmt())
                return true;
            if (Expr())
            {
                if (matchToken("DELIMITADOR_PUNTO_COMA"))
                    return true;
            }

            return value;
        }

        private bool ForStmt()
        {
            bool value = false;

            if (matchToken("PALABRA_RESERVADA_FOR"))
                if (matchToken("PARENTESIS_ABRE"))
                    if (Expr_hijo())
                        if (matchToken("DELIMITADOR_PUNTO_COMA"))
                            if(Expr())
                                if(matchToken("DELIMITADOR_PUNTO_COMA"))
                                    if(Expr_hijo())
                                        if(matchToken("PARENTESIS_CIERRA"))
                                            if(Stmt())
                                                return true;

            return value;
        }

        private bool ReturnStmt()
        {
            bool value = false;

            if (matchToken("PALABRA_RESERVADA_RETURN"))
            {
                if (Expr_hijo())
                    return true;
            }

            return value;
        }

        private bool LValue()
        {
            bool value = false;

            if (matchToken("IDENTIFICADOR"))
            {
                return true;
            }
            else if (Expr())
            {
                if (LValue_hijo())
                    return true;
            }

            return value;
        }

        private bool LValue_hijo()
        {
            bool value = false;

            if (matchToken("DELIMITADOR_PUNTO"))
            {
                if (matchToken("IDENTIFICADOR"))
                    return true;
            }
            else if (matchToken("CORCHETE_ABRE"))
            {
                if (Expr())
                {
                    if (matchToken("CORCHETE_CIERRA"))
                        return true;
                }
            }

            return value;
        }

        private bool Expr()
        {
            bool value = false;


            
            return value;
        }

        private bool Expr_hijo()
        {
            bool value = true;

            if (Expr())
                return true;

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
