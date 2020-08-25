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
    public partial class Form1 : Form
    {
        String texto;
        OpenFileDialog open;
        AnalizadorLexico archivoLectura;
        StreamWriter file;
        string rutaEscritura;
        public Form1()
        {
            InitializeComponent();
            open = new OpenFileDialog();
        }

        private void btnCargarArchivo_Click(object sender, EventArgs e)
        {
            open.DefaultExt = ".java";
            open.Filter = "Java Files (*.java)|*.java|All files (*.*)|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                txtRuta.Text = open.FileName;
                archivoLectura = new AnalizadorLexico();
                texto = File.ReadAllText(open.FileName);
                rutaEscritura = Environment.CurrentDirectory + @"\" + Path.GetFileNameWithoutExtension(open.FileName) + ".out";
                AnalizarCodigo(open.FileName);
                EscribirArchivo(rutaEscritura);
            }
        }


        private void EscribirArchivo(string ruta) {
            file = new StreamWriter(ruta);
            StringBuilder sb;
            try {
                foreach (ListViewItem lvi in lvToken.Items)
                {
                    sb = new StringBuilder();

                    foreach (ListViewItem.ListViewSubItem listViewSubItem in lvi.SubItems)
                    {
                        sb.Append(string.Format("{0}\t", listViewSubItem.Text));
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

        private void AnalizarCodigo(string ruta)
        {
            lvToken.View = View.Details;
            using (StreamReader sr = new StreamReader(ruta))
            {
                //tbxCode.Text = sr.ReadToEnd();

                archivoLectura.AddTokenRule(@"\s+", "ESPACIO", true);
                archivoLectura.AddTokenRule(@"(void|int|double|boolean|string|class|const|interface|null|this|extends|implements|for|while|if|else|return|break|New|System|out|println)\b", "PALABRA_RESERVADA");
                archivoLectura.AddTokenRule(@"(true|false)\b", "CONSTANTE_BOOLEANA");
                archivoLectura.AddTokenRule(@"\b[_$a-zA-Z][_$a-zA-Z0-9]*\b", "IDENTIFICADOR");
                archivoLectura.AddTokenRule("\".*?\"", "CADENA");
                archivoLectura.AddTokenRule("//[^\r\n]*", "COMENTARIO1");
                archivoLectura.AddTokenRule("/[*](.*?|\n|\r)*[*]/", "COMENTARIO2");
                archivoLectura.AddTokenRule(@"(\d+\.\d*([eE][\+\-]?\d*)?\s)", "CONSTANTE_DOUBLE");
                archivoLectura.AddTokenRule(@"\d+\b", "CONSTANTE_ENTERA_DECIMAL");
                archivoLectura.AddTokenRule(@"(0x|0X)[\da-fA-F]+\b", "CONSTANTE_ENTERA_HEXADECIMAL");
                //archivoLectura.AddTokenRule(@"'\\.'|'[^\\]'", "CARACTER");
                archivoLectura.AddTokenRule(@"[\(\)\{\}\[\];,]", "DELIMITADOR");
                archivoLectura.AddTokenRule(@"[\+\-\=/*%]", "OPERADOR");
                archivoLectura.AddTokenRule(@">|<|==|>=|<=|!", "COMPARADOR");

                archivoLectura.Compile(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture);
            }

            lvToken.Items.Clear();

            int n = 0, e = 0;

            foreach (var tk in archivoLectura.GetTokens(texto))
            {
                if (tk.Nombre == "ERROR")
                {
                    e++;
                }

                ListViewItem lvi = new ListViewItem();
                string[] row = { tk.Nombre, tk.Lexema, tk.Linea.ToString(), tk.Columna.ToString(), tk.Index.ToString()};
                var listViewItem = new ListViewItem(row);
                lvToken.Items.Add(listViewItem);
                n++;
            }
        }
    }
}
