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
        }

        /// <summary>
        /// Agrega un nuevo token que contiene una expresion regular
        /// </summary>
        /// <param name="expresion_regular">expresión regular con la que debe coincidir</param>
        /// <param name="nombre_token">id único para esta expresión regular</param>
        /// <param name="ignorar">true para omitir la expresión regular</param>
        public void agregarToken(string expresion_regular, string nombre_token, bool ignorar = false)
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
        public IEnumerable<Token> obtenerTokens(string texto)
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
        public void cargarExpresionesRegulares(RegexOptions options)
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
    }
}
