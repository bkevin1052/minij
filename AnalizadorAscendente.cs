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
        private List<Token> tokens;
        private List<Token> tokensErroneos;
        private Stack<int> pEstados;
        private Stack<Token> pSimbolos;
        private int iTokenIndex;
        private bool bReduccion;

        public AnalizadorAscendente()
        {
            this.lProducciones = new List<Produccion>();
            this.dTablaAnalisis = new Dictionary<Validacion, Accion>();
            tokens = new List<Token>();
            tokensErroneos = new List<Token>();
            pEstados = new Stack<int>();
            pSimbolos = new Stack<Token>();
            iTokenIndex = default;
            bReduccion = default;
        }

        public AnalizadorAscendente(List<Token> listaTokens)
        {
            this.lProducciones = new List<Produccion>();
            this.dTablaAnalisis = new Dictionary<Validacion, Accion>();
            tokens = listaTokens;
            tokensErroneos = new List<Token>();
            pEstados = new Stack<int>();
            pSimbolos = new Stack<Token>();
            iTokenIndex = 0;
            bReduccion = false;
        }

        public bool analizar()
        {
            bool value = false;

            pEstados.Push(0);
            bReduccion = false;


            return value;
        }

        public List<Token> getErrores()
        {
            return this.tokensErroneos;
        }
    }
}
