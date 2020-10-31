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

            pEstados.Push(0);
            bReduccion = false;
            aActual.setAccion(new Accion());

            while(aActual.sRegla != "Aceptar" && iTokenIndex < lTokens.Count())
            {
                comparacion();
            }

            return value;
        }

        public bool comparacion()
        {
            bool value = false;

            Accion aAux = dTablaAnalisis[new Validacion(lTokens[iTokenIndex].Lexema, pEstados.Peek())];

            if(aAux != null)
            {

            }
            else
            {
                lTokensErroneos.Add(lTokens[iTokenIndex]);
                iTokenIndex++;
            }

            return value;
        }

        public List<Token> getErrores()
        {
            return this.lTokensErroneos;
        }
    }
}
