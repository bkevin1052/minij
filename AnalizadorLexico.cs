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
        List<string> TNames;
        int[] GNumbers;

        public AnalizadorLexico()
        {
            requiereCompilar = true;
            TNames = new List<string>();
        }

        /// <summary>
        /// Agrega una nueva regla para reconocer token
        /// </summary>
        /// <param name="expresion_regular">patrón en el que debe encajar</param>
        /// <param name="nombre_token">id único para este patrón</param>
        /// <param name="ignorar">true para no devolver este token</param>
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
                TNames.Add(nombre_token);

            requiereCompilar = true;
        }


        /// <summary>
        /// Analisa una entrada en busca de tokens validos y errores
        /// </summary>
        /// <param name="texto">entrada a analizar</param>
        public IEnumerable<Token> obtenerTokens(string texto)
        {
            if (requiereCompilar) throw new Exception("Compilación Requerida, llame al método Compile(options).");

            Match match = rex.Match(texto);

            if (!match.Success) yield break;

            int line = 1, start = 0, index = 0;

            while (match.Success)
            {
                if (match.Index > index)
                {
                    string token = texto.Substring(index, match.Index - index);

                    yield return new Token("ERROR", token, index, line, (index - start) + 1);

                    line += CountNewLines(token, index, ref start);
                }

                for (int i = 0; i < GNumbers.Length; i++)
                {
                    if (match.Groups[GNumbers[i]].Success)
                    {
                        string name = rex.GroupNameFromNumber(GNumbers[i]);

                        yield return new Token(name, match.Value, match.Index, line, (match.Index - start) + 1);

                        break;
                    }
                }

                line += CountNewLines(match.Value, match.Index, ref start);
                index = match.Index + match.Length;
                match = match.NextMatch();
            }

            if (texto.Length > index)
            {
                yield return new Token("ERROR", texto.Substring(index), index, line, (index - start) + 1);
            }
        }

        /// <summary>
        /// Crea el AFN con los patrones establecidos 
        /// </summary>
        public void Compile(RegexOptions options)
        {
            if (patron == null) throw new Exception("Agrege uno o más patrones, llame al método AddTokenRule(pattern, token_name).");

            if (requiereCompilar)
            {
                try
                {
                    rex = new Regex(patron.ToString(), options);

                        GNumbers = new int[TNames.Count];
                    string[] gn = rex.GetGroupNames();

                    for (int i = 0, idx = 0; i < gn.Length; i++)
                    {
                        if (TNames.Contains(gn[i]))
                        {
                            GNumbers[idx++] = rex.GroupNumberFromName(gn[i]);
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
        private int CountNewLines(string token, int index, ref int line_start)
        {
            int line = 0;

            for (int i = 0; i < token.Length; i++)
                if (token[i] == '\n')
                {
                    line++;
                    line_start = index + i + 1;
                }

            return line;
        }
    }
}
