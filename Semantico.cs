using frmMain.semantico.ejecucion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minij
{
    public partial class Semantico : Form
    {
        public Semantico()
        {
            InitializeComponent();
        }

        private void btnCompilar_Click(object sender, EventArgs e)
        {
            ejecutar();
        }

        private void ejecutar()
        {
            // analisis lexico

            string codigo = txtTexto.Text;
            var lexer = new ThanosLexer(codigo);

            while (lexer.HasNext())
            {
                Console.WriteLine($"{lexer.CurrentToken} = {lexer.CurrentLexeme}");
            }

            if (!lexer.IsSuccessful)
            {
                MessageBox.Show("Error de analisis lexico");
                return;
            }

            // analisis semantico

            var parser = new ThanosParser(lexer.Tokens);
            string nuevoCodigo = parser.ParseToSourceCode();

            //MessageBox.Show(nuevoCodigo);

            // ejecucion

            if (!ThanosExecutor.Compilar(nuevoCodigo))
            {
                MessageBox.Show(String.Join("\n\n", ThanosExecutor.Errores));
                return;
            }

            // MessageBox.Show("Ejecutando...");

        }
    }
}
