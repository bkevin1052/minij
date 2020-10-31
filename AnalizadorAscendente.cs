using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using minij.Models;

namespace minij
{
    class AnalizadorAscendente
    {
        private List<Produccion> lProducciones;
        private Dictionary<Validacion, Accion> dTablaAnalisis;
        private List<Token> lTokens;
        private List<Token> lTokensErroneos;
        private Stack<int> pEstados;
        private Stack<Token> pSimbolos;
        private int iTokenIndex;
        private bool bReduccion;
        private Accion aActual;

        public AnalizadorAscendente()
        {
            this.lProducciones = new List<Produccion>();
            this.dTablaAnalisis = new Dictionary<Validacion, Accion>();
            lTokens = new List<Token>();
            lTokensErroneos = new List<Token>();
            pEstados = new Stack<int>();
            pSimbolos = new Stack<Token>();
            iTokenIndex = default;
            bReduccion = default;
            aActual = new Accion();
        }

        public AnalizadorAscendente(List<Token> listaTokens)
        {
            this.lProducciones = new List<Produccion>();
            this.dTablaAnalisis = new Dictionary<Validacion, Accion>();
            lTokens = listaTokens;
            lTokensErroneos = new List<Token>();
            pEstados = new Stack<int>();
            pSimbolos = new Stack<Token>();
            iTokenIndex = 0;
            bReduccion = false;
            aActual = new Accion();
        }

        public bool analizar()
        {
            bool value = false;

            lTokens.Add(new Token("SIMBOLO_FINAL_ARCHIVO", "KyAtodoBien", lTokens.Last().Linea + lTokens.Last().Lexema.Length + 1, lTokens.Last().Linea, lTokens.Last().Linea + lTokens.Last().Lexema.Length + 1));
            cargarGramatica();
            cargarProducciones();
            pEstados.Push(0);
            bReduccion = false;
            aActual.setAccion(new Accion());

            while(aActual.sRegla != "Aceptar" && iTokenIndex < lTokens.Count())
            {
                comparacion();

                if(aActual.sRegla == "Aceptar") { value = true; }
            }

            return value;
        }

        public bool comparacion()
        {
            bool value = false;

            if (bReduccion)
            {
                if (dTablaAnalisis.ContainsKey(new Validacion(pSimbolos.Peek().Lexema, pEstados.Peek())))
                {
                    aActual = dTablaAnalisis[new Validacion(pSimbolos.Peek().Lexema, pEstados.Peek())];

                    if (aActual.sRegla == "IrA")
                    {
                        pEstados.Push(aActual.iEstado);
                        bReduccion = false;
                    }
                }
            }
            else if(dTablaAnalisis.ContainsKey(new Validacion(lTokens[iTokenIndex].Nombre, pEstados.Peek())))
            {
                aActual = dTablaAnalisis[new Validacion(lTokens[iTokenIndex].Nombre, pEstados.Peek())];

                if(aActual.sRegla == "D")
                {
                    pEstados.Push(aActual.iEstado);
                    pSimbolos.Push(lTokens[iTokenIndex]);
                    iTokenIndex++;
                }
                else if(aActual.sRegla == "R")
                {
                    for (int i = 0; i < lProducciones[aActual.iEstado].iNumeroEstados; i++)
                    {
                        pEstados.Pop();
                        pSimbolos.Pop();
                    }
                    pSimbolos.Push(new Token(lProducciones[aActual.iEstado].sSimbolo, lProducciones[aActual.iEstado].sSimbolo, lProducciones[aActual.iEstado].iEstado, 0, 0));
                    bReduccion = true;
                }
            }
            else
            {
                lTokensErroneos.Add(lTokens[iTokenIndex]);
                iTokenIndex++;
            }

            if (aActual.sRegla == "Aceptar") { value = true; }

            return value;
        }


        private void cargarGramatica()
        {
            dTablaAnalisis.Add(new Validacion("PALABRA_RESERVADA_INT", 0), new Accion("D", 3));
            dTablaAnalisis.Add(new Validacion("PALABRA_RESERVADA_INT", 2), new Accion("D", 6));
            dTablaAnalisis.Add(new Validacion("PALABRA_RESERVADA_INT", 3), new Accion("D", 3));
            dTablaAnalisis.Add(new Validacion("PALABRA_RESERVADA_INT", 4), new Accion("R", 3));
            dTablaAnalisis.Add(new Validacion("PALABRA_RESERVADA_INT", 6), new Accion("D", 6));
            dTablaAnalisis.Add(new Validacion("PALABRA_RESERVADA_INT", 8), new Accion("R", 2));
            dTablaAnalisis.Add(new Validacion("PALABRA_RESERVADA_STRING", 0), new Accion("D", 4));
            dTablaAnalisis.Add(new Validacion("PALABRA_RESERVADA_STRING", 2), new Accion("D", 7));
            dTablaAnalisis.Add(new Validacion("PALABRA_RESERVADA_STRING", 3), new Accion("D", 4));
            dTablaAnalisis.Add(new Validacion("PALABRA_RESERVADA_STRING", 4), new Accion("R", 3));
            dTablaAnalisis.Add(new Validacion("PALABRA_RESERVADA_STRING", 6), new Accion("D", 7));
            dTablaAnalisis.Add(new Validacion("PALABRA_RESERVADA_STRING", 8), new Accion("R", 2));
            dTablaAnalisis.Add(new Validacion("SIMBOLO_FINAL_ARCHIVO", 1), new Accion("Aceptar", default));
            dTablaAnalisis.Add(new Validacion("SIMBOLO_FINAL_ARCHIVO", 5), new Accion("R", 1));
            dTablaAnalisis.Add(new Validacion("SIMBOLO_FINAL_ARCHIVO", 7), new Accion("R", 3));
            dTablaAnalisis.Add(new Validacion("SIMBOLO_FINAL_ARCHIVO", 9), new Accion("R", 2));
            dTablaAnalisis.Add(new Validacion("IrAS", 0), new Accion("IrA", 1));
            dTablaAnalisis.Add(new Validacion("IrAC", 0), new Accion("IrA", 2));
            dTablaAnalisis.Add(new Validacion("IrAC", 2), new Accion("IrA", 5));
            dTablaAnalisis.Add(new Validacion("IrAC", 3), new Accion("IrA", 8));
            dTablaAnalisis.Add(new Validacion("IrAC", 6), new Accion("IrA", 9));
        }

        private void cargarProducciones()
        {
            lProducciones.Add(new Produccion(0, "IrA"+"SPrima", 1));
            lProducciones.Add(new Produccion(1, "IrAS", 2));
            lProducciones.Add(new Produccion(2, "IrAC", 2));
            lProducciones.Add(new Produccion(3, "IrAC", 1));
        }

        public List<Token> getErrores()
        {
            return this.lTokensErroneos;
        }
    }
}
