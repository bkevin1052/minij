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
            this.btnCargarArchivo = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.rtbErrores = new System.Windows.Forms.RichTextBox();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCargarArchivo
            // 
            this.btnCargarArchivo.Location = new System.Drawing.Point(12, 12);
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
            this.lblError.Location = new System.Drawing.Point(12, 58);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(59, 17);
            this.lblError.TabIndex = 1;
            this.lblError.Text = "Errores:";
            // 
            // rtbErrores
            // 
            this.rtbErrores.Location = new System.Drawing.Point(12, 91);
            this.rtbErrores.Name = "rtbErrores";
            this.rtbErrores.Size = new System.Drawing.Size(333, 516);
            this.rtbErrores.TabIndex = 2;
            this.rtbErrores.Text = "";
            // 
            // txtRuta
            // 
            this.txtRuta.Location = new System.Drawing.Point(134, 16);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(924, 22);
            this.txtRuta.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 619);
            this.Controls.Add(this.txtRuta);
            this.Controls.Add(this.rtbErrores);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.btnCargarArchivo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCargarArchivo;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.RichTextBox rtbErrores;
        private System.Windows.Forms.TextBox txtRuta;
    }
}

