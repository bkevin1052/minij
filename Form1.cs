using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
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

        private void AnalizarCodigo(string ruta)
        {
            lvToken.View = View.Details;
            using (StreamReader sr = new StreamReader(ruta))
            {
                //tbxCode.Text = sr.ReadToEnd();

                archivoLectura.AddTokenRule(@"\s+", "ESPACIO", true);
                archivoLectura.AddTokenRule(@"(void|int|double|boolean|string|class|const|interface|null|this|extends|implements|for|while|if|else|return|break|New|System|out|println)\b", "PALABRA_RESERVADA");
                archivoLectura.AddTokenRule(@"(true|false)\b", "CONSTANTE_BOOLEANA");
                archivoLectura.AddTokenRule(@"\b[_$a-zA-Z][_$a-zA-Z0-9]{0,30}\b", "IDENTIFICADOR");
                archivoLectura.AddTokenRule("\".*?[^\n]\"", "CADENA");
                archivoLectura.AddTokenRule("//[^\r\n]*", "COMENTARIO1",true);
                archivoLectura.AddTokenRule("/[*](.*?|\n|\r)*[*]/", "COMENTARIO2",true);
                archivoLectura.AddTokenRule(@"/[*]|[*]/", "EOF_EN_COMENTARIO");
                archivoLectura.AddTokenRule("\".*?\n", "EOF_EN_CADENA");
                archivoLectura.AddTokenRule(@"(\d+\.\d*([eE][\+\-]?\d*)?\s)", "CONSTANTE_DOUBLE");
                archivoLectura.AddTokenRule(@"\d+\b", "CONSTANTE_ENTERA_DECIMAL");
                archivoLectura.AddTokenRule(@"(0x|0X)[\da-fA-F]+\b", "CONSTANTE_ENTERA_HEXADECIMAL");
                //archivoLectura.AddTokenRule(@"'\\.'|'[^\\]'", "CARACTER");
                archivoLectura.AddTokenRule(@"(\[\]|\{\}|\(\))", "DELIMITADOR_VACIO");
                archivoLectura.AddTokenRule(@"[\(\)\{\}\[\];,\.]", "DELIMITADOR");
                archivoLectura.AddTokenRule(@"(<|<=|>|>=|==|!|!=|&&|\|\|)(\w|\s)", "COMPARADOR");
                archivoLectura.AddTokenRule(@"[\+\-\=/*%]", "OPERADOR");
                

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
