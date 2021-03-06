﻿using frmMain;
using frmMain.semantico;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minij
{
    public partial class Semantico : Form
    {

        RegexLexer csLexer = new RegexLexer();
        List<String> PalabrasReservadas;
        TablaSimbolos ts = new TablaSimbolos();
        TablaSimbolos tabla;

        public Semantico()
        {
            InitializeComponent();

            txtTexto.Text = Form1.texto;
            txtTexto.ScrollBars = ScrollBars.Vertical;

            using (StreamReader sr = new StreamReader(@"..\..\RegexLexer.cs"))
            {

                //csLexer.AddTokenRule(@"\t+", "TABULACION");//esme
                csLexer.AddTokenRule(@"\s+", "ESPACIO", true);
                csLexer.AddTokenRule("\".*?\"", "CADENA");
                csLexer.AddTokenRule(@"'\\.'|'[^\\]'", "CARACTER");
                csLexer.AddTokenRule("//[^\r\n]*", "COMENTARIOLINEA");
                csLexer.AddTokenRule("/[*].*?[*]/", "COMENTARIOBLOQUE");
                csLexer.AddTokenRule(@"\d+\.\d+", "DECIMAL");//ESME
                csLexer.AddTokenRule(@"\d*\.?\d+", "NUMERO");
                csLexer.AddTokenRule(@"\b[_a-zA-Z][\w]*\.?\b[_a-zA-Z][\w]*\b", "IDENTIFICADOR_FUNCIONAL");//ESME
                csLexer.AddTokenRule(@"[\(\)\{\}\[\];,]", "DELIMITADOR");
                csLexer.AddTokenRule(@"[\.=\+\-/*%]", "OPERADOR");
                csLexer.AddTokenRule(@">|<|==|>=|<=|!|!=", "COMPARADOR");
                csLexer.AddTokenRule(@"\b[_a-zA-Z][\w]*\b", "IDENTIFICADOR");
                csLexer.AddTokenRule(@"\+\+|\-\-", "OPERADOR_INC_O_DEC");//esme
                csLexer.AddTokenRule(@"\+=|\-=|\*=|/=", "OPERADOR_ASIGNACION");//esme
                csLexer.AddTokenRule(@"[\.=\+\-/*%}|\^]", "OPERADOR");
                csLexer.AddTokenRule(@"\&\&|\|\||\!\=", "OPERADOR_LOGICO");//esme

                // csLexer.AddTokenRule();
                //listas  macoy
                PalabrasReservadas = new List<string>() { "abstract","assert","boolean","break","byte", "case", "catch",
                    "char","class","const","continue","default","do","double","else","enum","extends","final","finally",
                    "float","for","goto","if","implements","import","instanceof","int","interface","long","native","new",
                    "package","private","protected","public","return","short","static","stricftp","super","switch","synchronized",
                    "this","throw","throws","transient","try","void","volatile","while"
                };
                
                csLexer.Compile(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture);
            }

        }

        public void Dfs(ParseTreeNode raiz, List<ParseTreeNode> nodos)
        {
            nodos.Add(raiz);
            raiz.ChildNodes.ForEach(nodo =>
            {
                Dfs(nodo, nodos);
            });
        }

        public bool VerificarDuplicados(TablaSimbolos tabla)
        {
            var contadores = new Dictionary<string, int>();

            foreach (Simbolo simbolo in tabla.Simbolos)
            {
                string id = simbolo.Id;

                if (!contadores.ContainsKey(id))
                    contadores[id] = 0;

                contadores[id] += 1;

                if (contadores[id] > 1)
                    return false;
            }

            return true;
        }

        public Dictionary<string, int> ObtenerDuplicados(TablaSimbolos tabla) {
            var contadores = new Dictionary<string, int>();

            foreach (Simbolo simbolo in tabla.Simbolos)
            {
                string id = simbolo.Id;

                if (!contadores.ContainsKey(id))
                    contadores[id] = 0;

                contadores[id] += 1;

                if (contadores[id] > 1)
                    return contadores;
            }

            return null;
        }

        private void btnCompilar_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            //aCodigo();
            analisisSemantico();
            //ejecutar();

        }

        public void analisisSemantico()
        {
            tabla = new TablaSimbolos();
            ParseTree parseTree = Sintactico.AnalisisSemantico(txtTexto.Text);

            if (parseTree.Root == null)
            {
                MessageBox.Show("Analisis Sintactico Incorrecto", "Información");
                listBox1.Items.Add("Error al analizar sintacticamente el texto.");
                txtTexto.ForeColor = Color.Red;
                return;
            }
            else {
                MessageBox.Show("Analisis Sintactico Correcto", "Información");
            }

            // hacer arbol
            var arbol = new Arbol(parseTree);

            // verificar tabla de simbolos
            ts = GenerarTablaSimbolos(arbol);

            bool duplicados = VerificarDuplicados(ts);
            Dictionary<string, int> cont = ObtenerDuplicados(ts);

            if (duplicados == false)
            {
                for (int i = 0; i < cont.Count; i++)
                {
                    if (cont.ToList()[i].Value > 1) {
                        listBox1.Items.Add("Variables duplicadas y declaradas con el mismo nombre " + cont.ToList()[i].Key);
                    }
                }
            }

            bool tipos = VerficarTipos(ts);

            if (tipos == false)
            {
                //listBox1.Items.Add("Error en conversión de tipo");
            }

            var sb = new StringBuilder();

            foreach (var s in ts.Simbolos)
            {
                listBox2.Items.Add(s);
            }

            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("Analisis Semantico Correcto", "Información");
                txtTexto.ForeColor = Color.Blue;
            }
            else {
                MessageBox.Show("Analisis Semantico Inorrecto", "Información");
                txtTexto.ForeColor = Color.Red;
            }
        }

        public TablaSimbolos GenerarTablaSimbolos(Arbol arbol)
        {
            List<ParseTreeNode> nodos = arbol.Recorrer(Gramatica.NoTerminales.DeclaracionVariable);
            
            foreach (ParseTreeNode nodo in nodos)
            {
                List<Simbolo> simbolos = CrearSimbolos(arbol, nodo);
                tabla.AgregarSimbolos(simbolos);
            }

            return tabla;
        }

        public bool ValidarRegex(string cadena, string regex)
        {
            Match validacion = Regex.Match(cadena, regex);
            return validacion.Success;
        }


        private string ValorDe(TablaSimbolos tabla, string id)
        {
            Console.WriteLine("/ = / = / = / = / = / = / = / = / = / = / = / = / = / = / = / = / = / = / = / = / = /");
            Console.WriteLine($"Checando tipo del id '{id}'");

            Simbolo simbolo = tabla.BuscarSimbolo(id);

            Console.WriteLine($"El valor del id actual '{id}' es '{simbolo.Valor}'");

            if (id.Equals(simbolo.Valor))
                return null;

            if (simbolo.Valor == null)
                return null;

            /*if (ValidarRegex(simbolo.Valor, Gramatica.ExpresionesRegulares.IdRegex) && !ValidarRegex(simbolo.Valor, Gramatica.ExpresionesRegulares.StringRegex))
            {
                Console.WriteLine("Recursando...");
                return ValorDe(tabla, simbolo.Valor);
            }*/

            Console.WriteLine($"Se encontro el tipo del id '{id}'. Tipo es '{simbolo.Tipo}'");
            return simbolo.Valor;
        }

        public bool VerficarTipos(TablaSimbolos tabla)
        {
            foreach (Simbolo simbolo in tabla.Simbolos)
            {
                string tipo = simbolo.Tipo;
                string valor = simbolo.Valor;
                string id = simbolo.Id;

                if (valor == null)
                    continue;

                // Si el valor del simbolo actual es un id
                if (ValidarRegex(valor, Gramatica.ExpresionesRegulares.IdRegex) && !ValidarRegex(valor, Gramatica.ExpresionesRegulares.StringRegex))
                {
                    // Primero, checamos si el identificador existe
                    if (!tabla.ContieneSimbolo(id))
                        return false;

                    // Despues, tenemos que obtener el valor de dicho id para comprobar su tipo
                    valor = ValorDe(tabla, id);
                }

                switch (tipo)
                {
                    case Gramatica.Terminales.Int:
                        {
                            if (!ValidarRegex(valor, Gramatica.ExpresionesRegulares.NumeroRegex))
                            {
                                listBox1.Items.Add("Error en ASIGNACIÓN de TIPO INT con variable: "+id);
                                return false;
                            }
                            if (valor.Contains('.'))
                            {
                                listBox1.Items.Add("Error en ASIGNACIÓN de TIPO INT con variable: " + id);
                                return false;
                            }
                            break;
                        }

                    case Gramatica.Terminales.Float:
                        {
                            if (!ValidarRegex(valor, Gramatica.ExpresionesRegulares.NumeroRegex))
                            {
                                listBox1.Items.Add("Error en ASIGNACIÓN de TIPO FLOAT con variable: " + id);
                                return false;
                            }
                            break;
                        }

                    case Gramatica.Terminales.Double:
                        {
                            if (!ValidarRegex(valor, Gramatica.ExpresionesRegulares.NumeroRegex))
                            {
                                listBox1.Items.Add("Error en ASIGNACIÓN de TIPO DOUBLE con variable: " + id);
                                return false;
                            }
                            break;
                        }

                    case Gramatica.Terminales.Bool:
                        {
                            if (!valor.Equals(Gramatica.Terminales.True) && !valor.Equals(Gramatica.Terminales.False))
                            {
                                listBox1.Items.Add("Error en ASIGNACIÓN de TIPO BOOL con variable: " + id);
                                return false;
                            }
                            break;
                        }

                    case Gramatica.Terminales.String:
                        {
                            if (!ValidarRegex(valor, Gramatica.ExpresionesRegulares.StringRegex))
                            {
                                listBox1.Items.Add("Error en ASIGNACIÓN de TIPO STRING con variable: " + id);
                                return false;
                            }
                            break;
                        }
                }
            }

            return true;
        }

        private List<Simbolo> CrearSimbolos(Arbol arbol, ParseTreeNode nodo)
        {
            var simbolos = new List<Simbolo>();

            List<ParseTreeNode> tipos = arbol.Recorrer(nodo, Gramatica.NoTerminales.Tipo);
            List<ParseTreeNode> ids = arbol.Recorrer(nodo, Gramatica.ExpresionesRegulares.Id);
            List<ParseTreeNode> asignaciones = arbol.Recorrer(nodo, Gramatica.NoTerminales.Asignable);
            var listaAsignables = new List<List<ParseTreeNode>>();

            asignaciones.ForEach(node =>
            {
                List<ParseTreeNode> hojas = arbol.HojasDe(node);
                listaAsignables.Add(hojas);
            });

            // Crear simbolos
            //for (int i = 0; i < ids.Count; i++)
            for (int i = 0; i < 1; i++)
            {
                string tipo = "";

                if (tipos[0].ChildNodes.Count > 1)
                {
                    tipo = tipos[0].FindTokenAndGetText() + " " + tipos[0].ChildNodes[1];
                }
                else {
                    tipo = tipos[0].FindTokenAndGetText();
                }              

                
                string id = ids[i].FindTokenAndGetText();

                if (listaAsignables.Count == 0)
                    simbolos.Add(new Simbolo(tipo, id));

                else
                {
                    var sb = new StringBuilder();

                    listaAsignables[0].ForEach(token =>
                    {
                        sb.Append($"{token.FindTokenAndGetText()} ");
                    });

                    string asignable = sb.ToString().Trim();
                    if (tipo == "int") {
                        asignable = OperacionDeTipoInt(asignable, id);
                    }else if (tipo == "double")
                    {
                        asignable = OperacionDeTipoDouble(asignable, id);
                    }else if (tipo == "float")
                    {
                        asignable = OperacionDeTipoFloat(asignable, id);
                    }
                    simbolos.Add(new Simbolo(tipo, id, asignable));
                }
            }

            return simbolos;
        }

        public string OperacionDeTipoInt(string asignable, string id) {
            string asignableTemp = asignable;
            string[] operacion = null;
            int temp = 0;
            int tempMulti = 0;
            //En el caso sea una operacion numerica
            if (asignable.Contains("+"))
            {
                operacion = asignable.Split('+');
                for (int j = 0; j < operacion.Length; j++)
                {
                    try
                    {
                        temp += int.Parse(operacion[j]);
                        asignable = temp.ToString();
                    }
                    catch
                    {
                        try
                        {
                            Simbolo simbolo = tabla.BuscarSimbolo(operacion[j].Trim());

                            if (simbolo != null)
                            {
                                if (simbolo.Tipo == "int" && simbolo.Valor != null)
                                {
                                    temp = temp + int.Parse(simbolo.Valor);
                                    asignable = temp.ToString();
                                }
                                else
                                {
                                    asignable = asignableTemp;
                                    listBox1.Items.Add("Error en CONVERSIÓN de SUMA con variable: " + id);
                                }
                            }
                            else
                            {
                                listBox1.Items.Add("Error en CONVERSIÓN de SUMA con variable: " + id);
                            }
                        }
                        catch {
                            listBox1.Items.Add("Error en CONVERSIÓN de SUMA con variable: " + id);
                        }
                    }
                }
            }
            else if (asignable.Contains("-"))
            {
                operacion = asignable.Split('-');
                for (int j = 0; j < operacion.Length; j++)
                {
                    try
                    {
                        temp -= int.Parse(operacion[j]);
                        asignable = temp.ToString();
                    }
                    catch
                    {
                        try
                        {
                            Simbolo simbolo = tabla.BuscarSimbolo(operacion[j].Trim());

                            if (simbolo != null)
                            {
                                if (simbolo.Tipo == "int" && simbolo.Valor != null)
                                {
                                    temp = temp - int.Parse(simbolo.Valor);
                                    asignable = temp.ToString();
                                }
                                else
                                {
                                    asignable = asignableTemp;
                                    listBox1.Items.Add("Error en CONVERSIÓN de RESTA con variable: " + id);
                                }
                            }
                            else
                            {
                                listBox1.Items.Add("Error en CONVERSIÓN de RESTA con variable: " + id);
                            }
                        }
                        catch
                        {
                            listBox1.Items.Add("Error en CONVERSIÓN de RESTA con variable: " + id);
                        }
                    }
                }
            }
            else if (asignable.Contains("*"))
            {
                operacion = asignable.Split('*');
                for (int j = 0; j < operacion.Length; j++)
                {
                    try
                    {
                        tempMulti = int.Parse(operacion[j].Trim());
                        tempMulti = tempMulti * int.Parse(operacion[1].Trim());
                        asignable = tempMulti.ToString();
                        j++;

                    }
                    catch
                    {
                        try
                        {
                            Simbolo simbolo = tabla.BuscarSimbolo(operacion[j].Trim());
                            if (simbolo != null)
                            {

                                if (simbolo.Tipo == "int" && simbolo.Valor != null)
                                {
                                    tempMulti = tempMulti * int.Parse(simbolo.Valor);
                                    asignable = temp.ToString();
                                }
                                else
                                {
                                    asignable = asignableTemp;
                                    listBox1.Items.Add("Error en CONVERSIÓN de MULTIPLICACIÓN con variable: " + id);
                                }
                            }
                            else
                            {
                                listBox1.Items.Add("Error en CONVERSIÓN de MULTIPLICACIÓN con variable: " + id);
                            }
                        }
                        catch {
                            listBox1.Items.Add("Error en CONVERSIÓN de MULTIPLICACIÓN con variable: " + id);
                        }
                    }
                }
            }
            else if (asignable.Contains("/"))
            {
                operacion = asignable.Split('/');
                for (int j = 0; j < operacion.Length; j++)
                {
                    try
                    {
                        tempMulti = int.Parse(operacion[j]);
                        tempMulti = tempMulti / int.Parse(operacion[1]);
                        asignable = tempMulti.ToString();
                        j++;
                    }
                    catch
                    {
                        try
                        {
                            Simbolo simbolo = tabla.BuscarSimbolo(operacion[j].Trim());
                            if (simbolo != null)
                            {

                                if (simbolo.Tipo == "int" && simbolo.Valor != null)
                                {
                                    tempMulti = tempMulti / int.Parse(simbolo.Valor);
                                    asignable = temp.ToString();
                                }
                                else
                                {
                                    asignable = asignableTemp;
                                    listBox1.Items.Add("Error en CONVERSIÓN de DIVISIÓN con variable: " + id);
                                }
                            }
                            else
                            {
                                listBox1.Items.Add("Error en CONVERSIÓN de DIVISIÓN con variable: " + id);
                            }
                        }
                        catch {
                            listBox1.Items.Add("Error en CONVERSIÓN de DIVISIÓN con variable: " + id);
                        }
                    }
                }
            }

            return asignable;
        }
        public string OperacionDeTipoDouble(string asignable, string id)
        {
            string asignableTemp = asignable;
            string[] operacion = null;
            double temp = 0;
            double tempMulti = 0;
            //En el caso sea una operacion numerica
            if (asignable.Contains("+"))
            {
                operacion = asignable.Split('+');
                for (int j = 0; j < operacion.Length; j++)
                {
                    try
                    {
                        temp += double.Parse(operacion[j]);
                        asignable = temp.ToString();
                    }
                    catch
                    {
                        try
                        {
                            Simbolo simbolo = tabla.BuscarSimbolo(operacion[j].Trim());

                            if (simbolo != null)
                            {
                                if ((simbolo.Tipo == "double" || simbolo.Tipo == "int" || simbolo.Tipo == "float") && simbolo.Valor != null)
                                {
                                    temp = temp + double.Parse(simbolo.Valor);
                                    asignable = temp.ToString();
                                }
                                else
                                {
                                    asignable = asignableTemp;
                                    listBox1.Items.Add("Error en CONVERSIÓN de SUMA con variable: " + id);
                                }
                            }
                            else
                            {
                                listBox1.Items.Add("Error en CONVERSIÓN de SUMA con variable: " + id);
                            }
                        }
                        catch
                        {
                            listBox1.Items.Add("Error en CONVERSIÓN de SUMA con variable: " + id);
                        }
                    }
                }
            }
            else if (asignable.Contains("-"))
            {
                operacion = asignable.Split('-');
                for (int j = 0; j < operacion.Length; j++)
                {
                    try
                    {
                        temp -= double.Parse(operacion[j]);
                        asignable = temp.ToString();
                    }
                    catch
                    {
                        try
                        {
                            Simbolo simbolo = tabla.BuscarSimbolo(operacion[j].Trim());

                            if (simbolo != null)
                            {
                                if ((simbolo.Tipo == "double" || simbolo.Tipo == "int" || simbolo.Tipo == "float") && simbolo.Valor != null)
                                {
                                    temp = temp - double.Parse(simbolo.Valor);
                                    asignable = temp.ToString();
                                }
                                else
                                {
                                    asignable = asignableTemp;
                                    listBox1.Items.Add("Error en CONVERSIÓN de RESTA con variable: " + id);
                                }
                            }
                            else
                            {
                                listBox1.Items.Add("Error en CONVERSIÓN de RESTA con variable: " + id);
                            }
                        }
                        catch
                        {
                            listBox1.Items.Add("Error en CONVERSIÓN de RESTA con variable: " + id);
                        }
                    }
                }
            }
            else if (asignable.Contains("*"))
            {
                operacion = asignable.Split('*');
                for (int j = 0; j < operacion.Length; j++)
                {
                    try
                    {
                        tempMulti = int.Parse(operacion[j].Trim());
                        tempMulti = tempMulti * double.Parse(operacion[1].Trim());
                        asignable = tempMulti.ToString();
                        j++;
                    }
                    catch
                    {
                        try
                        {
                            Simbolo simbolo = tabla.BuscarSimbolo(operacion[j].Trim());
                            if (simbolo != null)
                            {

                                if ((simbolo.Tipo == "double" || simbolo.Tipo == "int" || simbolo.Tipo == "float") && simbolo.Valor != null)
                                {
                                    tempMulti = tempMulti * double.Parse(simbolo.Valor);
                                    asignable = temp.ToString();
                                }
                                else
                                {
                                    asignable = asignableTemp;
                                    listBox1.Items.Add("Error en CONVERSIÓN de MULTIPLICACIÓN con variable: " + id);
                                }
                            }
                            else
                            {
                                listBox1.Items.Add("Error en CONVERSIÓN de MULTIPLICACIÓN con variable: " + id);
                            }
                        }
                        catch
                        {
                            listBox1.Items.Add("Error en CONVERSIÓN de MULTIPLICACIÓN con variable: " + id);
                        }
                    }
                }
            }
            else if (asignable.Contains("/"))
            {
                operacion = asignable.Split('/');
                for (int j = 0; j < operacion.Length; j++)
                {
                    try
                    {
                        tempMulti = double.Parse(operacion[j]);
                        tempMulti = tempMulti / double.Parse(operacion[1]);
                        asignable = tempMulti.ToString();
                        j++;
                    }
                    catch
                    {
                        try
                        {
                            Simbolo simbolo = tabla.BuscarSimbolo(operacion[j].Trim());
                            if (simbolo != null)
                            {

                                if ((simbolo.Tipo == "double" || simbolo.Tipo == "int" || simbolo.Tipo == "float") && simbolo.Valor != null)
                                {
                                    tempMulti = tempMulti / double.Parse(simbolo.Valor);
                                    asignable = temp.ToString();
                                }
                                else
                                {
                                    asignable = asignableTemp;
                                    listBox1.Items.Add("Error en CONVERSIÓN de DIVISIÓN con variable: " + id);
                                }
                            }
                            else
                            {
                                listBox1.Items.Add("Error en CONVERSIÓN de DIVISIÓN con variable: " + id);
                            }
                        }
                        catch
                        {
                            listBox1.Items.Add("Error en CONVERSIÓN de DIVISIÓN con variable: " + id);
                        }
                    }
                }
            }

            return asignable;
        }
        public string OperacionDeTipoFloat(string asignable, string id)
        {
            string asignableTemp = asignable;
            string[] operacion = null;
            float temp = 0;
            float tempMulti = 0;
            //En el caso sea una operacion numerica
            if (asignable.Contains("+"))
            {
                operacion = asignable.Split('+');
                for (int j = 0; j < operacion.Length; j++)
                {
                    try
                    {
                        temp += float.Parse(operacion[j]);
                        asignable = temp.ToString();
                    }
                    catch
                    {
                        try
                        {
                            Simbolo simbolo = tabla.BuscarSimbolo(operacion[j].Trim());

                            if (simbolo != null)
                            {
                                if ((simbolo.Tipo == "double" || simbolo.Tipo == "int" || simbolo.Tipo == "float") && simbolo.Valor != null)
                                {
                                    temp = temp + float.Parse(simbolo.Valor);
                                    asignable = temp.ToString();
                                }
                                else
                                {
                                    asignable = asignableTemp;
                                    listBox1.Items.Add("Error en CONVERSIÓN de SUMA con variable: " + id);
                                }
                            }
                            else
                            {
                                listBox1.Items.Add("Error en CONVERSIÓN de SUMA con variable: " + id);
                            }
                        }
                        catch
                        {
                            listBox1.Items.Add("Error en CONVERSIÓN de SUMA con variable: " + id);
                        }
                    }
                }
            }
            else if (asignable.Contains("-"))
            {
                operacion = asignable.Split('-');
                for (int j = 0; j < operacion.Length; j++)
                {
                    try
                    {
                        temp -= float.Parse(operacion[j]);
                        asignable = temp.ToString();
                    }
                    catch
                    {
                        try
                        {
                            Simbolo simbolo = tabla.BuscarSimbolo(operacion[j].Trim());

                            if (simbolo != null)
                            {
                                if ((simbolo.Tipo == "double" || simbolo.Tipo == "int" || simbolo.Tipo == "float") && simbolo.Valor != null)
                                {
                                    temp = temp - float.Parse(simbolo.Valor);
                                    asignable = temp.ToString();
                                }
                                else
                                {
                                    asignable = asignableTemp;
                                    listBox1.Items.Add("Error en CONVERSIÓN de RESTA con variable: " + id);
                                }
                            }
                            else
                            {
                                listBox1.Items.Add("Error en CONVERSIÓN de RESTA con variable: " + id);
                            }
                        }
                        catch
                        {
                            listBox1.Items.Add("Error en CONVERSIÓN de RESTA con variable: " + id);
                        }
                    }
                }
            }
            else if (asignable.Contains("*"))
            {
                operacion = asignable.Split('*');
                for (int j = 0; j < operacion.Length; j++)
                {
                    try
                    {
                        tempMulti = float.Parse(operacion[j].Trim());
                        tempMulti = tempMulti * float.Parse(operacion[1].Trim());
                        asignable = tempMulti.ToString();
                        j++;
                    }
                    catch
                    {
                        try
                        {
                            Simbolo simbolo = tabla.BuscarSimbolo(operacion[j].Trim());
                            if (simbolo != null)
                            {

                                if ((simbolo.Tipo == "double" || simbolo.Tipo == "int" || simbolo.Tipo == "float") && simbolo.Valor != null)
                                {
                                    tempMulti = tempMulti * float.Parse(simbolo.Valor);
                                    asignable = temp.ToString();
                                }
                                else
                                {
                                    asignable = asignableTemp;
                                    listBox1.Items.Add("Error en CONVERSIÓN de MULTIPLICACIÓN con variable: " + id);
                                }
                            }
                            else
                            {
                                listBox1.Items.Add("Error en CONVERSIÓN de MULTIPLICACIÓN con variable: " + id);
                            }
                        }
                        catch
                        {
                            listBox1.Items.Add("Error en CONVERSIÓN de MULTIPLICACIÓN con variable: " + id);
                        }
                    }
                }
            }
            else if (asignable.Contains("/"))
            {
                operacion = asignable.Split('/');
                for (int j = 0; j < operacion.Length; j++)
                {
                    try
                    {
                        tempMulti = float.Parse(operacion[j]);
                        tempMulti = tempMulti / float.Parse(operacion[1]);
                        asignable = tempMulti.ToString();
                    }
                    catch
                    {
                        try
                        {
                            Simbolo simbolo = tabla.BuscarSimbolo(operacion[j].Trim());
                            if (simbolo != null)
                            {

                                if ((simbolo.Tipo == "double" || simbolo.Tipo == "int" || simbolo.Tipo == "float") && simbolo.Valor != null)
                                {
                                    tempMulti = tempMulti / float.Parse(simbolo.Valor);
                                    asignable = temp.ToString();
                                }
                                else
                                {
                                    asignable = asignableTemp;
                                    listBox1.Items.Add("Error en CONVERSIÓN de DIVISIÓN con variable: " + id);
                                }
                            }
                            else
                            {
                                listBox1.Items.Add("Error en CONVERSIÓN de DIVISIÓN con variable: " + id);
                            }
                        }
                        catch
                        {
                            listBox1.Items.Add("Error en CONVERSIÓN de DIVISIÓN con variable: " + id);
                        }
                    }
                }
            }

            return asignable;
        }
    }
}
