using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace minij
{
    public class AnalizadorLexico
    {
        Regex rex;
        StringBuilder patron;
        bool requiereCompilar;
        List<string> expresionesValidas;
        int[] indiceTokenValido;

        public AnalizadorLexico()
        {
            requiereCompilar = true;
            expresionesValidas = new List<string>();
            ingresarExpresionesRegulares();
        }

        /// <summary>
        /// Agrega un nuevo token que contiene una expresion regular
        /// </summary>
        /// <param name="expresion_regular">expresión regular con la que debe coincidir</param>
        /// <param name="nombre_token">id único para esta expresión regular</param>
        /// <param name="ignorar">true para omitir la expresión regular</param>
        private void agregarToken(string expresion_regular, string nombre_token, bool ignorar = false)
        {
            if (string.IsNullOrWhiteSpace(nombre_token))
                throw new ArgumentException(string.Format("{0} no es un nombre válido.", nombre_token));

            if (string.IsNullOrEmpty(expresion_regular))
                throw new ArgumentException(string.Format("El patrón {0} no es válido.", expresion_regular));

            if (patron == null)
                patron = new StringBuilder(string.Format("(?<{0}>{1})", nombre_token, expresion_regular));
            else
                patron.Append(string.Format("|(?<{0}>{1})", nombre_token, expresion_regular));

            if (!ignorar)
                expresionesValidas.Add(nombre_token);

            requiereCompilar = true;
        }


        /// <summary>
        /// Analiza una entrada en busca de tokens validos y errores
        /// </summary>
        /// <param name="texto">entrada a analizar</param>
        private IEnumerable<Token> obtenerTokens(string texto)
        {
            if (requiereCompilar) throw new Exception("Necesita cargar las ER, llame al método cargarExpresionesRegulares(options).");

            Match match = rex.Match(texto);

            if (!match.Success) yield break;

            int linea = 1, inicio = 0, indice = 0;

            while (match.Success)
            {
                if (match.Index > indice)
                {
                    string token = texto.Substring(indice, match.Index - indice);

                    yield return new Token("ERROR", token, indice, linea, (indice - inicio) + 1);

                    linea += contarLineas(token, indice, ref inicio);
                }

                for (int i = 0; i < indiceTokenValido.Length; i++)
                {
                    if (match.Groups[indiceTokenValido[i]].Success)
                    {
                        string name = rex.GroupNameFromNumber(indiceTokenValido[i]);

                        yield return new Token(name, match.Value, match.Index, linea, (match.Index - inicio) + 1);

                        break;
                    }
                }

                linea += contarLineas(match.Value, match.Index, ref inicio);
                indice = match.Index + match.Length;
                match = match.NextMatch();
            }

            if (texto.Length > indice)
            {
                yield return new Token("ERROR", texto.Substring(indice), indice, linea, (indice - inicio) + 1);
            }
        }

        /// <summary>
        /// Crea el Automáta finito no determinista con las expresiones regulares establecidas 
        /// </summary>
        private void cargarExpresionesRegulares(RegexOptions options)
        {
            if (patron == null) throw new Exception("Agrege una o más ER, llame al método agregarToken(expresion_regular, nombre_token).");

            if (requiereCompilar)
            {
                try
                {
                    rex = new Regex(patron.ToString(), options);

                        indiceTokenValido = new int[expresionesValidas.Count];
                    string[] lexico = rex.GetGroupNames();

                    for (int i = 0, idx = 0; i < lexico.Length; i++)
                    {
                        if (expresionesValidas.Contains(lexico[i]))
                        {
                            indiceTokenValido[idx] = rex.GroupNumberFromName(lexico[i]);
                            idx++;
                        }
                    }

                    requiereCompilar = false;
                }
                catch (Exception ex) { throw ex; }
            }
        }

        /// <summary>
        /// Cuenta la cantidad de lineas presentes en un token, establece el inicio de linea.
        /// </summary>
        private int contarLineas(string token, int indice, ref int inicioDeLinea)
        {
            int linea = 0;

            for (int i = 0; i < token.Length; i++)
                if (token[i] == '\n')
                {
                    linea++;
                    inicioDeLinea = indice + i + 1;
                }

            return linea;
        }

        /// <summary>
        /// Envia las expresiones regular para cargar la gramatica
        /// </summary>
        private void ingresarExpresionesRegulares()
        {
            //--ESPACIOS EN BLANCO
            agregarToken(@"\s+", "ESPACIO", true);
            //--COMENTARIOS
            agregarToken("//[^\r\n]*", "COMENTARIO1", true);
            agregarToken("/[*](.*?|\n|\r)*[*]/", "COMENTARIO2", true);
            agregarToken(@"\/[*](.*?|\n|\r)*$", "EOF_EN_COMENTARIO");
            //--PALABRAS RESERVADAS
            agregarToken(@"(KyAtodoBien)\b", "SIMBOLO_FINAL_ARCHIVO");
            agregarToken(@"(int)\b", "PALABRA_RESERVADA_INT");
            agregarToken(@"(double)\b", "PALABRA_RESERVADA_DOUBLE");
            agregarToken(@"(boolean)\b", "PALABRA_RESERVADA_BOOLEAN");
            agregarToken(@"(bool)\b", "PALABRA_RESERVADA_BOOL");
            agregarToken(@"(string)\b", "PALABRA_RESERVADA_STRING");
            agregarToken(@"(void)\b", "PALABRA_RESERVADA_VOID");
            agregarToken(@"(for)\b", "PALABRA_RESERVADA_FOR");
            agregarToken(@"(return)\b", "PALABRA_RESERVADA_RETURN");
            agregarToken(@"(this)\b", "PALABRA_RESERVADA_THIS");
            agregarToken(@"(New)\b", "PALABRA_RESERVADA_NEW");
            agregarToken(@"(null)\b", "PALABRA_RESERVADA_NULL");
            agregarToken(@"(static)\b", "PALABRA_RESERVADA_STATIC");
            agregarToken(@"(class)\b", "PALABRA_RESERVADA_CLASS");
            agregarToken(@"(interface)\b", "PALABRA_RESERVADA_INTERFACE");
            agregarToken(@"(if)\b", "PALABRA_RESERVADA_IF");
            agregarToken(@"(else)\b", "PALABRA_RESERVADA_ELSE");
            agregarToken(@"(while)\b", "PALABRA_RESERVADA_WHILE");
            agregarToken(@"(break)\b", "PALABRA_RESERVADA_BREAK");
            agregarToken(@"(extends)\b", "PALABRA_RESERVADA_EXTENDS");
            agregarToken(@"(implements)\b", "PALABRA_RESERVADA_IMPLEMENTS");
            agregarToken(@"(System)\b", "PALABRA_RESERVADA_System");
            agregarToken(@"(out)\b", "PALABRA_RESERVADA_OUT");
            agregarToken(@"(println)\b", "PALABRA_RESERVADA_PRINTLN");
            //--CONSTANTE BOOLEANAS
            agregarToken(@"(true|false)", "CONSTANTE_BOOLEANA");
            //--IDENTIFICADORES
            agregarToken(@"[_$a-zA-Z][_$a-zA-Z0-9]*", "IDENTIFICADOR");
            //--CADENAS
            agregarToken("\".*?[^\n]\"", "CADENA");
            agregarToken("\".*?\n", "EOF_EN_CADENA");
            //--CONSTANTES NUMERICAS
            agregarToken(@"(\d+\.\d*([eE][\+\-]?\d+)?)", "CONSTANTE_DOUBLE");
            agregarToken(@"\d+", "CONSTANTE_ENTERA_DECIMAL");
            agregarToken(@"(0x|0X)[\da-fA-F]+", "CONSTANTE_ENTERA_HEXADECIMAL");
            //agregarToken(@"'\\.'|'[^\\]'", "CARACTER");
            //--DELIMITADORES
            //agregarToken(@"(\(\))", "PARENTESIS_VACIO");
            agregarToken(@"(\[\])", "CORCHETE_VACIO");
            //agregarToken(@"(\{\})", "LLAVE_VACIO");
            agregarToken(@"[,]", "DELIMITADOR_COMA");
            agregarToken(@"[;]", "DELIMITADOR_PUNTO_COMA");
            agregarToken(@"[\.]", "DELIMITADOR_PUNTO");
            agregarToken(@"[\(]", "PARENTESIS_ABRE");
            agregarToken(@"[\)]", "PARENTESIS_CIERRA");
            agregarToken(@"[\[]", "CORCHETE_ABRE");
            agregarToken(@"[\]]", "CORCHETE_CIERRA");
            agregarToken(@"[\{]", "LLAVE_ABRE");
            agregarToken(@"[\}]", "LLAVE_CIERRA");
            //--COMPARADORES
            agregarToken(@"(<=)", "COMPARADOR_MENOR_IGUAL");
            agregarToken(@"(>=)", "COMPARADOR_MAYOR_IGUAL");
            agregarToken(@"(==)", "COMPARADOR_IGUAL_IGUAL");
            agregarToken(@"(!=)", "COMPARADOR_DIFERENTE_IGUAL");
            agregarToken(@"(&&)", "COMPARADOR_AND");
            agregarToken(@"(\|\|)", "COMPARADOR_OR");
            agregarToken(@"[<]", "COMPARADOR_MENOR");
            agregarToken(@"[>]", "COMPARADOR_MAYOR");
            agregarToken(@"[!]", "COMPARADOR_DIFERENTE");
            //--OPERADORES
            agregarToken(@"[\=]", "OPERADOR_IGUAL");
            agregarToken(@"[\+]", "OPERADOR_MAS");
            agregarToken(@"[\-]", "OPERADOR_MENOS");
            agregarToken(@"[\*]", "OPERADOR_MULT");
            agregarToken(@"[\/]", "OPERADOR_DIV");
            agregarToken(@"[%]", "OPERADOR_PORCENTAJE");


            //CARGAR EXPRESIONES REGULARES EN ESTRUCTURA REGEX
            cargarExpresionesRegulares(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture);
        }


        /// <summary>
        /// Funcion que retorna la lista de tokens encontrada dentro del analisis lexico
        /// </summary>
        /// <param name="texto">Texto que se desea analizar</param>
        /// <returns>Lista de tokens producidos en el analisis lexico</returns>
        public List<Token> obtenerTokensLexico(string texto)
        {
            List<Token> tokens = new List<Token>();

            foreach (var tk in obtenerTokens(texto))
            {
                if (tk.Lexema.Length > 31) { tk.Nombre = "ERROR - LARGO DE CADENA"; tk.Lexema = tk.Lexema.Substring(0, 31); }
                tokens.Add(tk);
            }

            return tokens;
        }

        /// <summary>
        /// Funcion que retorna la lista de tokens encontrada dentro del analisis lexico pero sin tomar en cuenta errores
        /// </summary>
        /// <param name="texto">Texto que se desea analizar</param>
        /// <returns>Lista de tokens para analisis sintactico</returns>
        public List<Token> obtenerTokensSintactico(string texto)
        {
            List<Token> tokens = new List<Token>();

            foreach (var tk in obtenerTokens(texto))
            {
                if (tk.Lexema.Length > 31) { tk.Nombre = "ERROR - LARGO DE CADENA"; tk.Lexema = tk.Lexema.Substring(0, 31); }
                if(!(tk.Nombre == "ERROR" || tk.Nombre == "EOF_EN_COMENTARIO" || tk.Nombre == "ERROR - LARGO DE CADENA" || tk.Nombre == "EOF_EN_CADENA"))
                tokens.Add(tk);
            }

            return tokens;
        }
    }
}
