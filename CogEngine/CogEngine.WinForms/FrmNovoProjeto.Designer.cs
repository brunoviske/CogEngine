namespace CogEngine.WinForms
{
    partial class FrmNovoProjeto
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
            this.components = new System.ComponentModel.Container();
            this.BtnCancelar = new System.Windows.Forms.Button();
            this.BtnCriarProjeto = new System.Windows.Forms.Button();
            this.LblNomeProjeto = new System.Windows.Forms.Label();
            this.LblPastaProjeto = new System.Windows.Forms.Label();
            this.TxtNomeProjeto = new System.Windows.Forms.TextBox();
            this.TxtPastaProjeto = new System.Windows.Forms.TextBox();
            this.BtnProcurarPasta = new System.Windows.Forms.Button();
            this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.ErrProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ErrProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Location = new System.Drawing.Point(242, 85);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(75, 23);
            this.BtnCancelar.TabIndex = 4;
            this.BtnCancelar.Text = "Cancelar";
            this.BtnCancelar.UseVisualStyleBackColor = true;
            this.BtnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // BtnCriarProjeto
            // 
            this.BtnCriarProjeto.Location = new System.Drawing.Point(134, 85);
            this.BtnCriarProjeto.Name = "BtnCriarProjeto";
            this.BtnCriarProjeto.Size = new System.Drawing.Size(102, 23);
            this.BtnCriarProjeto.TabIndex = 3;
            this.BtnCriarProjeto.Text = "Criar novo projeto";
            this.BtnCriarProjeto.UseVisualStyleBackColor = true;
            this.BtnCriarProjeto.Click += new System.EventHandler(this.BtnCriarProjeto_Click);
            // 
            // LblNomeProjeto
            // 
            this.LblNomeProjeto.AutoSize = true;
            this.LblNomeProjeto.Location = new System.Drawing.Point(10, 19);
            this.LblNomeProjeto.Name = "LblNomeProjeto";
            this.LblNomeProjeto.Size = new System.Drawing.Size(85, 13);
            this.LblNomeProjeto.TabIndex = 5;
            this.LblNomeProjeto.Text = "Nome do projeto";
            // 
            // LblPastaProjeto
            // 
            this.LblPastaProjeto.AutoSize = true;
            this.LblPastaProjeto.Location = new System.Drawing.Point(10, 46);
            this.LblPastaProjeto.Name = "LblPastaProjeto";
            this.LblPastaProjeto.Size = new System.Drawing.Size(84, 13);
            this.LblPastaProjeto.TabIndex = 6;
            this.LblPastaProjeto.Text = "Pasta do projeto";
            // 
            // TxtNomeProjeto
            // 
            this.TxtNomeProjeto.Location = new System.Drawing.Point(111, 16);
            this.TxtNomeProjeto.Name = "TxtNomeProjeto";
            this.TxtNomeProjeto.Size = new System.Drawing.Size(165, 20);
            this.TxtNomeProjeto.TabIndex = 0;
            this.TxtNomeProjeto.TextChanged += new System.EventHandler(this.TxtNomeProjeto_TextChanged);
            // 
            // TxtPastaProjeto
            // 
            this.TxtPastaProjeto.Location = new System.Drawing.Point(111, 43);
            this.TxtPastaProjeto.Name = "TxtPastaProjeto";
            this.TxtPastaProjeto.Size = new System.Drawing.Size(165, 20);
            this.TxtPastaProjeto.TabIndex = 1;
            // 
            // BtnProcurarPasta
            // 
            this.BtnProcurarPasta.Location = new System.Drawing.Point(284, 41);
            this.BtnProcurarPasta.Name = "BtnProcurarPasta";
            this.BtnProcurarPasta.Size = new System.Drawing.Size(24, 23);
            this.BtnProcurarPasta.TabIndex = 2;
            this.BtnProcurarPasta.Text = "...";
            this.BtnProcurarPasta.UseVisualStyleBackColor = true;
            this.BtnProcurarPasta.Click += new System.EventHandler(this.BtnProcurarPasta_Click);
            // 
            // ErrProvider
            // 
            this.ErrProvider.ContainerControl = this;
            // 
            // FrmNovoProjeto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 123);
            this.Controls.Add(this.BtnProcurarPasta);
            this.Controls.Add(this.TxtPastaProjeto);
            this.Controls.Add(this.TxtNomeProjeto);
            this.Controls.Add(this.LblPastaProjeto);
            this.Controls.Add(this.LblNomeProjeto);
            this.Controls.Add(this.BtnCriarProjeto);
            this.Controls.Add(this.BtnCancelar);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(353, 161);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(353, 161);
            this.Name = "FrmNovoProjeto";
            this.Text = "Novo Projeto";
            ((System.ComponentModel.ISupportInitialize)(this.ErrProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnCancelar;
        private System.Windows.Forms.Button BtnCriarProjeto;
        private System.Windows.Forms.Label LblNomeProjeto;
        private System.Windows.Forms.Label LblPastaProjeto;
        private System.Windows.Forms.TextBox TxtNomeProjeto;
        private System.Windows.Forms.TextBox TxtPastaProjeto;
        private System.Windows.Forms.Button BtnProcurarPasta;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
        private System.Windows.Forms.ErrorProvider ErrProvider;
    }
}