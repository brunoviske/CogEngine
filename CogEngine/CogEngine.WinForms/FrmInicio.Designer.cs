namespace CogEngine.WinForms
{
    partial class FrmInicio
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
            this.LblCog = new System.Windows.Forms.Label();
            this.LblEngine = new System.Windows.Forms.Label();
            this.BtnNovoProjeto = new System.Windows.Forms.Button();
            this.BtnAbrirProjeto = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LblCog
            // 
            this.LblCog.AutoSize = true;
            this.LblCog.Font = new System.Drawing.Font("Matura MT Script Capitals", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCog.Location = new System.Drawing.Point(131, 30);
            this.LblCog.Name = "LblCog";
            this.LblCog.Size = new System.Drawing.Size(182, 72);
            this.LblCog.TabIndex = 0;
            this.LblCog.Text = "COG";
            // 
            // LblEngine
            // 
            this.LblEngine.AutoSize = true;
            this.LblEngine.Font = new System.Drawing.Font("Lucida Handwriting", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblEngine.Location = new System.Drawing.Point(177, 100);
            this.LblEngine.Name = "LblEngine";
            this.LblEngine.Size = new System.Drawing.Size(91, 27);
            this.LblEngine.TabIndex = 1;
            this.LblEngine.Text = "Engine";
            // 
            // BtnNovoProjeto
            // 
            this.BtnNovoProjeto.Location = new System.Drawing.Point(82, 167);
            this.BtnNovoProjeto.Name = "BtnNovoProjeto";
            this.BtnNovoProjeto.Size = new System.Drawing.Size(95, 23);
            this.BtnNovoProjeto.TabIndex = 2;
            this.BtnNovoProjeto.Text = "Novo Projeto";
            this.BtnNovoProjeto.UseVisualStyleBackColor = true;
            this.BtnNovoProjeto.Click += new System.EventHandler(this.BtnNovoProjeto_Click);
            // 
            // BtnAbrirProjeto
            // 
            this.BtnAbrirProjeto.Location = new System.Drawing.Point(268, 167);
            this.BtnAbrirProjeto.Name = "BtnAbrirProjeto";
            this.BtnAbrirProjeto.Size = new System.Drawing.Size(95, 23);
            this.BtnAbrirProjeto.TabIndex = 3;
            this.BtnAbrirProjeto.Text = "Abrir Projeto";
            this.BtnAbrirProjeto.UseVisualStyleBackColor = true;
            this.BtnAbrirProjeto.Click += new System.EventHandler(this.BtnAbrirProjeto_Click);
            // 
            // FrmInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 211);
            this.Controls.Add(this.BtnAbrirProjeto);
            this.Controls.Add(this.BtnNovoProjeto);
            this.Controls.Add(this.LblEngine);
            this.Controls.Add(this.LblCog);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(444, 249);
            this.MinimumSize = new System.Drawing.Size(444, 249);
            this.Name = "FrmInicio";
            this.Text = "CogEngine";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmInicio_FormClosed);
            this.Load += new System.EventHandler(this.FrmInicio_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblCog;
        private System.Windows.Forms.Label LblEngine;
        private System.Windows.Forms.Button BtnNovoProjeto;
        private System.Windows.Forms.Button BtnAbrirProjeto;
    }
}