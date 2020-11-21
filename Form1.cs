using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace minij
{
    public partial class Form1 : Form
    {
        public static String texto;
        OpenFileDialog open;
        AnalizadorLexico analizador;
        AnalizadorSintactico sintactico;
        AnalizadorAscendente ascendente;
        StreamWriter file;
        string rutaEscritura;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            open = new OpenFileDialog();
        }

        private void btnCargarArchivo_Click(object sender, EventArgs e)
        {
            //open.DefaultExt = ".java";
            //open.Filter = "Java Files (*.java)|*.java|All files (*.*)|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                txtRuta.Text = open.FileName;
                analizador = new AnalizadorLexico();
                texto = File.ReadAllText(open.FileName);
                rutaEscritura = Environment.CurrentDirectory + @"\" + Path.GetFileNameWithoutExtension(open.FileName) + ".out";
                AnalizarCodigo(open.FileName);
                MessageBox.Show("Analisis Lexico Correcto", "Información");
                //EscribirArchivo(rutaEscritura);
            }
        }

        /// <summary>
        /// Crea un archivo de salida .out en donde se coloca cada token identificado y errores
        /// </summary>
        /// <param name="ruta">Ruta para crear o sobreescribir el archivo</param>
        private void EscribirArchivo(string ruta) {
            file = new StreamWriter(ruta);
            StringBuilder sb;
            int contador;
            try {
                foreach (ListViewItem lvi in lvToken.Items)
                {
                    sb = new StringBuilder();
                    contador = 0;
                    foreach (ListViewItem.ListViewSubItem listViewSubItem in lvi.SubItems)
                    {
                        if (contador == 0)
                        {
                            sb.Append(("\tToken: " + listViewSubItem.Text).PadRight(40));
                        }
                        else if (contador == 1)
                        {
                            sb.Append(("Lexema: " + listViewSubItem.Text).PadRight(100));
                        }
                        else if (contador == 2)
                        {
                            sb.Append(("Linea: " + listViewSubItem.Text).PadRight(10));
                        }
                        else if (contador == 3)
                        {
                            sb.Append(("Columna: " + listViewSubItem.Text).PadRight(20));
                        }
                        else if (contador == 4) {
                            sb.Append(("Indice: " + listViewSubItem.Text).PadRight(20));
                        }
                        contador++;
                    }
                    file.WriteLine(sb.ToString());
                }
                file.WriteLine();
                file.Close();
            } catch(Exception e) {
                file.WriteLine(e.Message);
                file.Close();
            }
            
        }

        /// <summary>
        /// Realiza un analisis lexico del texto ingresado, desde el archivo selecionado
        /// </summary>
        /// <param name="ruta">Ruta del archivo a analizar</param>
        private void AnalizarCodigo(string ruta)
        {
            lvToken.View = View.Details;
            lvToken.Items.Clear();

            foreach(var tk in analizador.obtenerTokensLexico(texto))
            {
                ListViewItem lvi = new ListViewItem();
                string[] row = { tk.Nombre, tk.Lexema, tk.Linea.ToString(), tk.Columna.ToString(), tk.Index.ToString() };
                var listViewItem = new ListViewItem(row);
                lvToken.Items.Add(listViewItem);
            }

            var listaTokens = analizador.obtenerTokensSintactico(texto);
            //analizarSintactico(listaTokens);
            //analizarAscendente(listaTokens);
        }

        /// <summary>
        /// Realiza un analisis sintactica a partir de los tokens generados por el analizador lexico
        /// </summary>
        /// <param name="tokens">Lista de tokens generada en la fase de analisis lexico</param>
        private void analizarSintactico(List<Token> tokens)
        {
            lvToken.View = View.Details;
            lvToken.Items.Clear();

            sintactico = new AnalizadorSintactico(tokens);
            if(sintactico.analizar())
            {
                MessageBox.Show("Analisis Sintactico - Positivo");
            }
            else
            {
                MessageBox.Show("Analisis Sintactico - Fallido");

                foreach (var tk in sintactico.tokensErroneos)
                {
                    ListViewItem lvi = new ListViewItem();
                    string[] row = { tk.Nombre, tk.Lexema, tk.Linea.ToString(), tk.Columna.ToString(), tk.Index.ToString() };
                    var listViewItem = new ListViewItem(row);
                    lvToken.Items.Add(listViewItem);
                }
            }
        }

        private void analizarAscendente(List<Token> tokens)
        {
            lvToken.View = View.Details;
            lvToken.Items.Clear();

            ascendente = new AnalizadorAscendente(tokens);

            if (ascendente.analizar())
            {
                MessageBox.Show("Analisis Sintactico - Positivo");
            }
            else
            {
                MessageBox.Show("Analisis Sintactico - Fallido");
                foreach (var tk in ascendente.getErrores())
                {
                    ListViewItem lvi = new ListViewItem();
                    string[] row = { tk.Nombre, tk.Lexema, tk.Linea.ToString(), tk.Columna.ToString(), tk.Index.ToString() };
                    var listViewItem = new ListViewItem(row);
                    lvToken.Items.Add(listViewItem);
                }
            }
        }

        private void btnSemantico_Click(object sender, EventArgs e)
        {
            var consola = new Semantico();
            consola.Show();
        }
    }
}
