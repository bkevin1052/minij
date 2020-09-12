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
        public List<Token> tokensErroneos;

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


        public bool analizar()
        {
            bool value = false;

            while (lookAhead <= tokens.Count - 1)
            {
                value = true;
                if (!Program())
                {
                    tokensErroneos.Add(tokens[lookAhead]);
                    matchToken(tokens[lookAhead].Nombre);
                    value = false;
                }
            }

            return value;
        }

        private bool Program()
        {
            bool value = false;
            int indiceActual = tokenActual;

            if (Decl())
                if (Program_hijo())
                    return true;

            resetIndice(indiceActual);
            return value;
        }

        private bool Program_hijo()
        {
            bool value = true;
            int indiceActual = tokenActual;

            if (Decl())
                if (Program_hijo())
                    return true;

            resetIndice(indiceActual);
            return value;
        }

        private bool Decl()
        {
            bool value = false;
            int indiceActual = tokenActual;

            if (VariableDecl())
                return true;
            if (FunctionDecl())
                return true;

            resetIndice(indiceActual);
            return value;
        }

        private bool VariableDecl()
        {
            bool value = false;
            int indiceActual = tokenActual;

            if (Variable())
                if (matchToken("DELIMITADOR_PUNTO_COMA"))
                    return true;

            resetIndice(indiceActual);
            return value;
        }

        private bool Variable()
        {
            bool value = false;
            int indiceActual = tokenActual;

            if (Type())
                if (matchToken("IDENTIFICADOR"))
                    return true;

            resetIndice(indiceActual);
            return value;
        }

        private bool Type()
        {
            bool value = false;
            int indiceActual = tokenActual;

            if (matchToken("PALABRA_RESERVADA_INT"))
            {
                if (Type_hijo())
                    return true;
            }
            if (matchToken("PALABRA_RESERVADA_DOUBLE"))
            {
                if (Type_hijo())
                    return true;
            }
            if (matchToken("PALABRA_RESERVADA_BOOLEAN"))
            {
                if (Type_hijo())
                    return true;
            }
            if (matchToken("PALABRA_RESERVADA_BOOL"))
            {
                if (Type_hijo())
                    return true;
            }
            if (matchToken("PALABRA_RESERVADA_STRING"))
            {
                if (Type_hijo())
                    return true;
            }
            if (matchToken("IDENTIFICADOR"))
            {
                if (Type_hijo())
                    return true;
            }

            resetIndice(indiceActual);
            return value;
        }

        private bool Type_hijo()
        {
            bool value = true;
            int indiceActual = tokenActual;

            if (matchToken("CORCHETE_VACIO"))
                if (Type_hijo())
                    return true;

            resetIndice(indiceActual);
            return value;
        }

        private bool FunctionDecl()
        {
            bool value = false;
            int indiceActual = tokenActual;

            if (matchToken("PALABRA_RESERVADA_VOID"))
            {
                if (Function())
                    return true;
            }
            else if(Type())
            {
                if (Function())
                    return true;
            }

            resetIndice(indiceActual);
            return value;
        }

        private bool Function()
        {
            bool value = false;
            int indiceActual = tokenActual;

            if (matchToken("IDENTIFICADOR"))
                if (matchToken("PARENTESIS_ABRE"))
                    if (Formals())
                        if (matchToken("PARENTESIS_CIERRA"))
                            if (Function_hijo())
                                return true;

            resetIndice(indiceActual);
            return value;
        }

        private bool Function_hijo()
        {
            bool value = true;
            int indiceActual = tokenActual;

            if (Stmt())
                if (Function_hijo())
                    return true;

            resetIndice(indiceActual);
            return value;
        }

        private bool Formals()
        {
            bool value = true;
            int indiceActual = tokenActual;

            if (Variable())
                if (Formals_hijo())
                        return true;

            resetIndice(indiceActual);
            return value;
        }

        private bool Formals_hijo()
        {
            bool value = true;
            int indiceActual = tokenActual;

            if (matchToken("DELIMITADOR_COMA"))
                if (Variable())
                    if (Formals_hijo())
                        return true;

            resetIndice(indiceActual);
            return value;
        }

        private bool Stmt()
        {
            bool value = false;
            int indiceActual = tokenActual;

            if (ForStmt())
                return true;
            if (ReturnStmt())
                return true;
            if (Expr())
            {
                if (matchToken("DELIMITADOR_PUNTO_COMA"))
                    return true;
            }

            resetIndice(indiceActual);
            return value;
        }

        private bool ForStmt()
        {
            bool value = false;
            int indiceActual = tokenActual;

            if (matchToken("PALABRA_RESERVADA_FOR"))
                if (matchToken("PARENTESIS_ABRE"))
                    if (Expr_nulo())
                        if (matchToken("DELIMITADOR_PUNTO_COMA"))
                            if(Expr())
                                if(matchToken("DELIMITADOR_PUNTO_COMA"))
                                    if(Expr_nulo())
                                        if(matchToken("PARENTESIS_CIERRA"))
                                            if(Stmt())
                                                return true;

            resetIndice(indiceActual);
            return value;
        }

        private bool ReturnStmt()
        {
            bool value = false;
            int indiceActual = tokenActual;

            if (matchToken("PALABRA_RESERVADA_RETURN"))
            {
                if (Expr_nulo())
                    if (matchToken("DELIMITADOR_PUNTO_COMA"))
                        return true;
            }

            resetIndice(indiceActual);
            return value;
        }

        private bool LValue()
        {
            bool value = false;
            int indiceActual = tokenActual;

            if (matchToken("IDENTIFICADOR"))
                if(LValue_hijo())
                    return true;
            
            resetIndice(indiceActual);
            return value;
        }

        private bool LValue_hijo()
        {
            bool value = true;
            int indiceActual = tokenActual;

            if (matchToken("DELIMITADOR_PUNTO"))
            {
                if (matchToken("IDENTIFICADOR"))
                    if(LValue_hijo())
                        return true;
            }
            if (matchToken("CORCHETE_ABRE"))
            {
                if (Expr())
                    if (matchToken("CORCHETE_CIERRA"))
                        if (LValue_hijo())
                            return true;
            }

            resetIndice(indiceActual);
            return value;
        }

        private bool Expr()
        {
            bool value = false;
            int indiceActual = tokenActual;

            if (Or())
                if (Expr_hijo())
                    return true;

            resetIndice(indiceActual);
            return value;
        }

        private bool Expr_hijo()
        {
            bool value = true;
            int indiceActual = tokenActual;

            if (matchToken("COMPARADOR_OR"))
            {
                if (Or())
                    if (Expr_hijo())
                        return true;
            }

            resetIndice(indiceActual);
            return value;
        }

        private bool Or()
        {
            bool value = false;
            int indiceActual = tokenActual;

            if (And())
                if (Or_hijo())
                    return true;

            resetIndice(indiceActual);
            return value;
        }

        private bool Or_hijo()
        {
            bool value = true;
            int indiceActual = tokenActual;

            if (matchToken("COMPARADOR_AND"))
            {
                if (And())
                    if (Or_hijo())
                        return true;
            }

            resetIndice(indiceActual);
            return value;
        }

        private bool And()
        {
            bool value = false;
            int indiceActual = tokenActual;

            if (Igualdad())
                if (And_hijo())
                    return true;

            resetIndice(indiceActual);
            return value;
        }

        private bool And_hijo()
        {
            bool value = true;
            int indiceActual = tokenActual;

            if (matchToken("COMPARADOR_DIFERENTE_IGUAL"))
            {
                if (Igualdad())
                    if (And_hijo())
                        return true;
            }
            if (matchToken("COMPARADOR_IGUAL_IGUAL"))
            {
                if (Igualdad())
                    if (And_hijo())
                        return true;
            }

            resetIndice(indiceActual);
            return value;
        }

        private bool Igualdad()
        {
            bool value = false;
            int indiceActual = tokenActual;

            if (Relacionales())
                if (Igualdad_hijo())
                    return true;

            resetIndice(indiceActual);
            return value;
        }

        private bool Igualdad_hijo()
        {
            bool value = true;
            int indiceActual = tokenActual;

            if (matchToken("COMPARADOR_MENOR_IGUAL"))
            {
                if (Relacionales())
                    if (Igualdad_hijo())
                        return true;
            }
            if (matchToken("COMPARADOR_MAYOR_IGUAL"))
            {
                if (Relacionales())
                    if (Igualdad_hijo())
                        return true;
            }
            if (matchToken("COMPARADOR_MENOR"))
            {
                if (Relacionales())
                    if (Igualdad_hijo())
                        return true;
            }
            if (matchToken("COMPARADOR_MAYOR"))
            {
                if (Relacionales())
                    if (Igualdad_hijo())
                        return true;
            }

            resetIndice(indiceActual);
            return value;
        }

        private bool Relacionales()
        {
            bool value = false;
            int indiceActual = tokenActual;

            if (Terminos())
                if (Relacionales_hijo())
                    return true;

            resetIndice(indiceActual);
            return value;
        }

        private bool Relacionales_hijo()
        {
            bool value = true;
            int indiceActual = tokenActual;

            if (matchToken("OPERADOR_MAS"))
            {
                if (Terminos())
                    if (Relacionales_hijo())
                        return true;
            }
            if (matchToken("OPERADOR_MENOS"))
            {
                if (Terminos())
                    if (Relacionales_hijo())
                        return true;
            }

            resetIndice(indiceActual);
            return value;
        }

        private bool Terminos()
        {
            bool value = false;
            int indiceActual = tokenActual;

            if (Multiplicadores())
                if (Terminos_hijo())
                    return true;

            resetIndice(indiceActual);
            return value;
        }

        private bool Terminos_hijo()
        {
            bool value = true;
            int indiceActual = tokenActual;

            if (matchToken("OPERADOR_MULT"))
            {
                if (Multiplicadores())
                    if (Terminos_hijo())
                        return true;
            }
            if (matchToken("OPERADOR_DIV"))
            {
                if (Multiplicadores())
                    if (Terminos_hijo())
                        return true;
            }
            if (matchToken("OPERADOR_PORCENTAJE"))
            {
                if (Multiplicadores())
                    if (Terminos_hijo())
                        return true;
            }

            resetIndice(indiceActual);
            return value;
        }

        private bool Multiplicadores()
        {
            bool value = false;
            int indiceActual = tokenActual;

            if (matchToken("OPERADOR_MENOS"))
            {
                if (Unitarios())
                    return true;
            }
            if (matchToken("COMPARADOR_DIFERENTE"))
            {
                if (Unitarios())
                    return true;
            }
            if (Unitarios())
                return true;

            resetIndice(indiceActual);
            return value;
        }

        private bool Unitarios()
        {
            bool value = false;
            int indiceActual = tokenActual;

            if (matchToken("PALABRA_RESERVADA_THIS"))
                return true;
            if (matchToken("PALABRA_RESERVADA_NEW"))
            {
                if (matchToken("PARENTESIS_ABRE"))
                    if (matchToken("IDENTIFICADOR"))
                        if (matchToken("PARENTESIS_CIERRA"))
                            return true;
            }
            if (Constant())
                return true;
            if (LValue())
            {
                if (Unitarios_hijo())
                    return true;
            }
            if (matchToken("PARENTESIS_ABRE"))
            {
                if (Expr())
                    if (matchToken("PARENTESIS_CIERRA"))
                        return true;
            }

            resetIndice(indiceActual);
            return value;
        }

        private bool Unitarios_hijo()
        {
            bool value = true;
            int indiceActual = tokenActual;

            if (matchToken("OPERADOR_IGUAL"))
                if(Expr())
                    return true;

            resetIndice(indiceActual);
            return value;
        }

        private bool Expr_nulo()
        {
            bool value = true;
            int indiceActual = tokenActual;

            if (Expr())
                return true;

            resetIndice(indiceActual);
            return value;
        }

        private bool Constant()
        {
            bool value = false;
            int indiceActual = tokenActual;

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

            resetIndice(indiceActual);
            return value;
        }

        private bool matchToken(string type)
        {
            bool value = false;
            if (lookAhead == tokens.Count())
                return false;
            if (tokens[lookAhead].Nombre == type)
            {
                tokenActual = lookAhead;
                lookAhead++;
                value = true;
            }

            return value;
        }

        private void resetIndice(int indice)
        {
            tokenActual = indice;
            lookAhead = tokenActual + 1;
        }
    }
}
