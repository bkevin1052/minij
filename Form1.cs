using Parser.Lexical;
using Parser.Models;
using Parser.Parse;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using Parser.LLTable;
using Action = Parser.State.Action;
using Color = System.Drawing.Color;
using TreeNode = Parser.Parse.TreeNode;
using System.Text;
using Parser.States;
using Parser;

namespace minij
{
    public partial class Form1 : Form
    {
        String texto;
        OpenFileDialog open;
        AnalizadorLexico analizador;
        AnalizadorSintactico sintactico;
        StreamWriter file;
        string rutaEscritura;

        private GrammarRules _grammarRules;
        private Preprocessor _preprocessor;
        private LeftToRight_RightMost_Zero _lrZero;
        private LeftToRight_LookAhead_One _leftToRightLookAhead1;

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
            analizarSintactico(listaTokens);
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

        private void btnGramatica_Click(object sender, EventArgs e)
        {
            ChooseFile(txtGramatica);
            btnParseGrammar_Click(null, null);
        }

        private void ChooseFile(TextBox textbox)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                CheckFileExists = true,
                AddExtension = true,
                Multiselect = false,
                CheckPathExists = true,
                DefaultExt = "txt",
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
            };

            openFile.ShowDialog();
            textbox.Text = openFile.FileName;
        } 

        private void btnParseGrammar_Click(object sender, EventArgs e)
        {
            listBoxGrammar.Items.Clear();
            listBoxFirst.Items.Clear();
            if (string.IsNullOrEmpty(txtGramatica.Text))
            {
                MessageBox.Show("Grammar file is empty!");
                return;
            }

            var text = File.ReadAllText(txtGramatica.Text);
            LexicalAnalyzer lex = new LexicalAnalyzer(text);
            _grammarRules = lex.TokenizeGrammar();
            foreach (ISymbol symbol in _grammarRules.SymbolList)
            {
                if (symbol.SymbolType == SymbolType.Variable)
                    listBoxGrammar.Items.Add(((Variable)symbol).ShowRules());
            }

            listBoxFirst.Items.Clear();
            listBoxFollow.Items.Clear();
            if (_grammarRules == null)
            {
                MessageBox.Show("Grammar File is empty");
                return;
            }
            _preprocessor = new Preprocessor(_grammarRules);
            _preprocessor.CalculateAllFirsts();
            _preprocessor.CalculateAllFollows();
            foreach (ISymbol symbol in _grammarRules.SymbolList)
            {
                if (symbol is Variable variable)
                {
                    listBoxFirst.Items.Add(variable.ShowFirsts());
                    listBoxFollow.Items.Add(variable.ShowFollows());
                }
            }
        }

        private void btnShowParseTree_Click(object sender, EventArgs e)
        {

        }

        private void btnFSM_Click(object sender, EventArgs e)
        {

        }

        private void cmbGrammarType_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabLR_0_Enter(null, null);
        }

        private void cmbGrammarType_SelectedIndexChanged_1(object sender, EventArgs e)
        {
        }

        private void tabLR_0_Enter(object sender, EventArgs e)
        {
            dataGridReportLR.Rows.Clear();
            dgvLR_0.Rows.Clear();
            dgvLR_0.Columns.Clear();

            LRType lrType = (LRType)cmbGrammarType.SelectedIndex;
            _lrZero = new LeftToRight_RightMost_Zero(_grammarRules, lrType, _preprocessor);
            txtLRStates.Text = _lrZero.CalculateStateMachine();
            var grammarTable = _lrZero.FillTable();
            foreach (var keyValuePair in _lrZero.MapperToNumber.MapTerminalToNumber)
            {
                dgvLR_0.Columns.Add(keyValuePair.Key, keyValuePair.Key);
            }
            foreach (var keyValuePair in _lrZero.MapperToNumber.MapVariableToNumber.Skip(1))
            {
                dgvLR_0.Columns.Add(keyValuePair.Key, keyValuePair.Key);
            }
            foreach (var keyValue in _lrZero.FiniteStateMachine.States)
            {
                dgvLR_0.Rows.Add(new DataGridViewRow()
                {
                    HeaderCell = { Value = keyValue.StateId },
                });
            }

            bool valid = true;
            foreach (var state in _lrZero.FiniteStateMachine.States)
            {
                for (int j = 0; j < _lrZero.MapperToNumber.TerminalCount; j++)
                {
                    var parserAction = grammarTable.ActionTable[state.StateId, j];
                    if (parserAction == null) continue;
                    dgvLR_0.Rows[state.StateId].Cells[j].Value = parserAction;
                    dgvLR_0.Rows[state.StateId].Cells[j].Style.BackColor = !parserAction.HasError ? Color.LightGreen : Color.Orange;
                    if (parserAction.HasError) valid = false;
                }

                int terminalCount = _lrZero.MapperToNumber.TerminalCount;
                for (int j = 0; j < _lrZero.MapperToNumber.VariableCount; j++)
                {
                    if (grammarTable.GoToTable[state.StateId, j] == null) continue;
                    dgvLR_0.Rows[state.StateId].Cells[j + terminalCount - 1].Value = grammarTable.GoToTable[state.StateId, j].StateId;
                    dgvLR_0.Rows[state.StateId].Cells[j + terminalCount - 1].Style.BackColor = Color.LightGreen;
                }
            }
            Progress<ParseReportModel> progress = new Progress<ParseReportModel>();
            progress.ProgressChanged += (o, m) =>
            {
                dataGridReportLR.Rows.Add(m.Stack, m.InputString, m.Output);
            };

            long stackTime = 0;
            if (valid)
            {
                var terminals = GetTerminals();
                if (terminals == null)
                {
                    MessageBox.Show("Terminal is empty!");
                    return;
                }
                _lrZero.Parse(terminals, progress);
            }
        }

        private List<Terminal> GetTerminals()
        {
            if (string.IsNullOrWhiteSpace(txtRuta.Text)) return null;
            return new LexicalAnalyzer(
                File.ReadAllText(txtRuta.Text)).TokenizeInputText();
        }

        private void tabItem_Enter(object sender, EventArgs e)
        {

        }

        private void btnFSM_Click_1(object sender, EventArgs e)
        {
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            form.WindowState = FormWindowState.Maximized;
            //create a viewer object 
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();

            //create a graph object 
            var graph = new Graph("Finite State Machine");
            //create the graph content 

            Dictionary<Parser.States.State, Node> dictionary = new Dictionary<Parser.States.State, Node>();
            foreach (Parser.States.State state in _lrZero.FiniteStateMachine.States)
            {
                Node node = new Node(state.ToStringCompact());
                node.Attr.FillColor = state.ReduceOnly ? Microsoft.Msagl.Drawing.Color.SeaGreen :
                                        (state.ShiftOnly ? Microsoft.Msagl.Drawing.Color.LightGreen : Microsoft.Msagl.Drawing.Color.Orange);

                dictionary.Add(state, node);
                graph.AddNode(node);
            }

            foreach (Parser.States.State state in _lrZero.FiniteStateMachine.States)
            {
                foreach (KeyValuePair<ISymbol, Parser.States.State> stateNextState in state.NextStates)
                {
                    var edge = new Edge(dictionary[state], dictionary[stateNextState.Value], ConnectionToGraph.Connected);
                    edge.LabelText = stateNextState.Key.ToString();
                    graph.AddPrecalculatedEdge(edge);
                }
            }

            viewer.Graph = graph;
            //associate the viewer with the form 
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            //show the form 
            form.ShowDialog();
        }

        private void btnShowParseTree_Click_1(object sender, EventArgs e)
        {
            Queue<TreeNode> nodes = new Queue<TreeNode>();
            foreach (TreeNode lrZeroNode in _lrZero.Nodes)
            {
                nodes.Enqueue(lrZeroNode);
            }
            ShowParseTree(nodes);
        }

        private void ShowParseTree(Queue<TreeNode> nodes)
        {
            var form = new Form();
            form.WindowState = FormWindowState.Maximized;
            GViewer viewer = new GViewer();
            var tree = new PhyloTree();


            while (nodes.Count > 0)
            {
                TreeNode treeNode = nodes.Dequeue();
                foreach (TreeNode childNode in treeNode.Nodes)
                {
                    Node node = tree.AddNode(treeNode.ToString());
                    node.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Orange;
                    tree.AddEdge(treeNode.ToString(), childNode.ToString());

                    nodes.Enqueue(childNode);
                }
            }

            viewer.Graph = tree;
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            form.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbGrammarType.SelectedIndexChanged -= cmbGrammarType_SelectedIndexChanged;
            cmbGrammarType.SelectedIndex = 0;
            cmbGrammarType.SelectedIndexChanged += cmbGrammarType_SelectedIndexChanged;
        }
    }
}
