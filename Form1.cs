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
        List<string> palabrasReservadas;

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
                AnalizarCodigo(open.FileName);
            }
        }

        private void AnalizarCodigo(string ruta)
        {
            lvToken.View = View.Details;
            using (StreamReader sr = new StreamReader(ruta))
            {
                //tbxCode.Text = sr.ReadToEnd();

                archivoLectura.AddTokenRule(@"\s+", "ESPACIO", true);
                archivoLectura.AddTokenRule(@"\b(public|void)\b", "PALABRA_RESERVADA");
                archivoLectura.AddTokenRule(@"\b[_a-zA-Z][\w]*\b", "IDENTIFICADOR");
                archivoLectura.AddTokenRule("\".*?\"", "CADENA");
                archivoLectura.AddTokenRule(@"'\\.'|'[^\\]'", "CARACTER");
                archivoLectura.AddTokenRule("//[^\r\n]*", "COMENTARIO1");
                archivoLectura.AddTokenRule("/[*].*?[*]/", "COMENTARIO2");
                archivoLectura.AddTokenRule(@"\d*\.?\d+", "NUMERO");
                archivoLectura.AddTokenRule(@"[\(\)\{\}\[\];,]", "DELIMITADOR");
                archivoLectura.AddTokenRule(@"[\.=\+\-/*%]", "OPERADOR");
                archivoLectura.AddTokenRule(@">|<|==|>=|<=|!", "COMPARADOR");
                

                palabrasReservadas = new List<string>() { "void","int","double","boolean","string",
                "class","const","interface","null","null","this","extends","implements","for","while",
                "if","else","return","New","System","out","println"};

                archivoLectura.Compile(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture);
            }

            lvToken.Items.Clear();

            int n = 0, e = 0;

            foreach (var tk in archivoLectura.GetTokens(texto))
            {
                if (tk.Nombre == "ERROR") e++;

                if (tk.Nombre == "IDENTIFICADOR")
                    if (palabrasReservadas.Contains(tk.Lexema))
                        tk.Nombre = "RESERVADO";

                ListViewItem lvi = new ListViewItem();
                string[] row = { tk.Nombre, tk.Lexema, tk.Linea.ToString(), tk.Columna.ToString(), tk.Index.ToString()};
                var listViewItem = new ListViewItem(row);
                lvToken.Items.Add(listViewItem);
                n++;
            }
        }
    }
}
