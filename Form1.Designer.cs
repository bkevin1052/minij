namespace minij
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnCargarArchivo = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.lvToken = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnGramatica = new System.Windows.Forms.Button();
            this.txtGramatica = new System.Windows.Forms.TextBox();
            this.listBoxGrammar = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxFirst = new System.Windows.Forms.ListBox();
            this.listBoxFollow = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cmbGrammarType = new System.Windows.Forms.ComboBox();
            this.dgvLR_0 = new System.Windows.Forms.DataGridView();
            this.dataGridReportLR = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtLRStates = new System.Windows.Forms.RichTextBox();
            this.btnFSM = new System.Windows.Forms.Button();
            this.btnShowParseTree = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLR_0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridReportLR)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCargarArchivo
            // 
            this.btnCargarArchivo.Location = new System.Drawing.Point(6, 5);
            this.btnCargarArchivo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCargarArchivo.Name = "btnCargarArchivo";
            this.btnCargarArchivo.Size = new System.Drawing.Size(116, 31);
            this.btnCargarArchivo.TabIndex = 0;
            this.btnCargarArchivo.Text = "Cargar archivo";
            this.btnCargarArchivo.UseVisualStyleBackColor = true;
            this.btnCargarArchivo.Click += new System.EventHandler(this.btnCargarArchivo_Click);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(6, 88);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(60, 17);
            this.lblError.TabIndex = 1;
            this.lblError.Text = "TOKEN:";
            // 
            // txtRuta
            // 
            this.txtRuta.Location = new System.Drawing.Point(128, 9);
            this.txtRuta.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(672, 22);
            this.txtRuta.TabIndex = 3;
            // 
            // lvToken
            // 
            this.lvToken.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lvToken.HideSelection = false;
            this.lvToken.Location = new System.Drawing.Point(9, 127);
            this.lvToken.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvToken.Name = "lvToken";
            this.lvToken.Size = new System.Drawing.Size(791, 445);
            this.lvToken.TabIndex = 4;
            this.lvToken.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Token";
            this.columnHeader1.Width = 160;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Lexema";
            this.columnHeader2.Width = 160;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Linea";
            this.columnHeader3.Width = 160;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Columna";
            this.columnHeader4.Width = 160;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Index";
            this.columnHeader5.Width = 160;
            // 
            // btnGramatica
            // 
            this.btnGramatica.Location = new System.Drawing.Point(6, 40);
            this.btnGramatica.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGramatica.Name = "btnGramatica";
            this.btnGramatica.Size = new System.Drawing.Size(116, 46);
            this.btnGramatica.TabIndex = 5;
            this.btnGramatica.Text = "Cargar Gramatica";
            this.btnGramatica.UseVisualStyleBackColor = true;
            this.btnGramatica.Click += new System.EventHandler(this.btnGramatica_Click);
            // 
            // txtGramatica
            // 
            this.txtGramatica.Location = new System.Drawing.Point(129, 52);
            this.txtGramatica.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtGramatica.Name = "txtGramatica";
            this.txtGramatica.Size = new System.Drawing.Size(671, 22);
            this.txtGramatica.TabIndex = 6;
            // 
            // listBoxGrammar
            // 
            this.listBoxGrammar.FormattingEnabled = true;
            this.listBoxGrammar.HorizontalScrollbar = true;
            this.listBoxGrammar.ItemHeight = 16;
            this.listBoxGrammar.Location = new System.Drawing.Point(817, 40);
            this.listBoxGrammar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBoxGrammar.Name = "listBoxGrammar";
            this.listBoxGrammar.Size = new System.Drawing.Size(241, 532);
            this.listBoxGrammar.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(814, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Gramatica:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1073, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "First Sets:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1314, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Follow Sets:";
            // 
            // listBoxFirst
            // 
            this.listBoxFirst.FormattingEnabled = true;
            this.listBoxFirst.HorizontalScrollbar = true;
            this.listBoxFirst.ItemHeight = 16;
            this.listBoxFirst.Location = new System.Drawing.Point(1064, 40);
            this.listBoxFirst.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBoxFirst.Name = "listBoxFirst";
            this.listBoxFirst.Size = new System.Drawing.Size(241, 532);
            this.listBoxFirst.TabIndex = 11;
            // 
            // listBoxFollow
            // 
            this.listBoxFollow.FormattingEnabled = true;
            this.listBoxFollow.HorizontalScrollbar = true;
            this.listBoxFollow.ItemHeight = 16;
            this.listBoxFollow.Location = new System.Drawing.Point(1311, 40);
            this.listBoxFollow.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBoxFollow.Name = "listBoxFollow";
            this.listBoxFollow.Size = new System.Drawing.Size(241, 532);
            this.listBoxFollow.TabIndex = 12;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1808, 613);
            this.tabControl1.TabIndex = 13;
            this.tabControl1.Enter += new System.EventHandler(this.tabItem_Enter);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnCargarArchivo);
            this.tabPage1.Controls.Add(this.listBoxFollow);
            this.tabPage1.Controls.Add(this.btnGramatica);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.listBoxFirst);
            this.tabPage1.Controls.Add(this.txtRuta);
            this.tabPage1.Controls.Add(this.txtGramatica);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.lblError);
            this.tabPage1.Controls.Add(this.listBoxGrammar);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.lvToken);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1800, 584);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Gramática";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnShowParseTree);
            this.tabPage2.Controls.Add(this.btnFSM);
            this.tabPage2.Controls.Add(this.txtLRStates);
            this.tabPage2.Controls.Add(this.dataGridReportLR);
            this.tabPage2.Controls.Add(this.dgvLR_0);
            this.tabPage2.Controls.Add(this.cmbGrammarType);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1800, 584);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "SLR";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cmbGrammarType
            // 
            this.cmbGrammarType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGrammarType.FormattingEnabled = true;
            this.cmbGrammarType.Items.AddRange(new object[] {
            "LR(0)",
            "SLR(1)",
            "CLR(1)"});
            this.cmbGrammarType.Location = new System.Drawing.Point(7, 7);
            this.cmbGrammarType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbGrammarType.Name = "cmbGrammarType";
            this.cmbGrammarType.Size = new System.Drawing.Size(245, 24);
            this.cmbGrammarType.TabIndex = 5;
            this.cmbGrammarType.SelectedIndexChanged += new System.EventHandler(this.cmbGrammarType_SelectedIndexChanged_1);
            // 
            // dgvLR_0
            // 
            this.dgvLR_0.AllowUserToAddRows = false;
            this.dgvLR_0.AllowUserToDeleteRows = false;
            this.dgvLR_0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLR_0.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLR_0.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLR_0.Location = new System.Drawing.Point(260, 7);
            this.dgvLR_0.Margin = new System.Windows.Forms.Padding(4);
            this.dgvLR_0.Name = "dgvLR_0";
            this.dgvLR_0.ReadOnly = true;
            this.dgvLR_0.RowHeadersWidth = 51;
            this.dgvLR_0.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvLR_0.Size = new System.Drawing.Size(583, 246);
            this.dgvLR_0.TabIndex = 6;
            // 
            // dataGridReportLR
            // 
            this.dataGridReportLR.AllowUserToAddRows = false;
            this.dataGridReportLR.AllowUserToDeleteRows = false;
            this.dataGridReportLR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridReportLR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridReportLR.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dataGridReportLR.Location = new System.Drawing.Point(260, 261);
            this.dataGridReportLR.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridReportLR.Name = "dataGridReportLR";
            this.dataGridReportLR.ReadOnly = true;
            this.dataGridReportLR.RowHeadersWidth = 51;
            this.dataGridReportLR.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridReportLR.Size = new System.Drawing.Size(583, 226);
            this.dataGridReportLR.TabIndex = 7;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "Stack";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "InputText";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "Result";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // txtLRStates
            // 
            this.txtLRStates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtLRStates.Location = new System.Drawing.Point(7, 39);
            this.txtLRStates.Margin = new System.Windows.Forms.Padding(4);
            this.txtLRStates.Name = "txtLRStates";
            this.txtLRStates.Size = new System.Drawing.Size(245, 451);
            this.txtLRStates.TabIndex = 8;
            this.txtLRStates.Text = "";
            // 
            // btnFSM
            // 
            this.btnFSM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFSM.Location = new System.Drawing.Point(7, 498);
            this.btnFSM.Margin = new System.Windows.Forms.Padding(4);
            this.btnFSM.Name = "btnFSM";
            this.btnFSM.Size = new System.Drawing.Size(247, 33);
            this.btnFSM.TabIndex = 9;
            this.btnFSM.Text = "View Finite State Machine";
            this.btnFSM.UseVisualStyleBackColor = true;
            this.btnFSM.Click += new System.EventHandler(this.btnFSM_Click_1);
            // 
            // btnShowParseTree
            // 
            this.btnShowParseTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowParseTree.Location = new System.Drawing.Point(262, 498);
            this.btnShowParseTree.Margin = new System.Windows.Forms.Padding(4);
            this.btnShowParseTree.Name = "btnShowParseTree";
            this.btnShowParseTree.Size = new System.Drawing.Size(583, 33);
            this.btnShowParseTree.TabIndex = 10;
            this.btnShowParseTree.Text = "Show Parse Tree";
            this.btnShowParseTree.UseVisualStyleBackColor = true;
            this.btnShowParseTree.Click += new System.EventHandler(this.btnShowParseTree_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1565, 619);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MiniJ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLR_0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridReportLR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCargarArchivo;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.ListView lvToken;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button btnGramatica;
        private System.Windows.Forms.TextBox txtGramatica;
        private System.Windows.Forms.ListBox listBoxGrammar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxFirst;
        private System.Windows.Forms.ListBox listBoxFollow;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnShowParseTree;
        private System.Windows.Forms.Button btnFSM;
        private System.Windows.Forms.RichTextBox txtLRStates;
        private System.Windows.Forms.DataGridView dataGridReportLR;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridView dgvLR_0;
        private System.Windows.Forms.ComboBox cmbGrammarType;
    }
}

