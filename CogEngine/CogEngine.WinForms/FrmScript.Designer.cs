namespace CogEngine.WinForms
{
    partial class FrmScript
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
            this.TxtNomeScript = new System.Windows.Forms.TextBox();
            this.LblScript = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.panelScript = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // TxtNomeScript
            // 
            this.TxtNomeScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtNomeScript.Location = new System.Drawing.Point(98, 16);
            this.TxtNomeScript.Name = "TxtNomeScript";
            this.TxtNomeScript.Size = new System.Drawing.Size(365, 20);
            this.TxtNomeScript.TabIndex = 1;
            // 
            // LblScript
            // 
            this.LblScript.AutoSize = true;
            this.LblScript.Location = new System.Drawing.Point(12, 20);
            this.LblScript.Name = "LblScript";
            this.LblScript.Size = new System.Drawing.Size(80, 13);
            this.LblScript.TabIndex = 2;
            this.LblScript.Text = "Nome do Script";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Código";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.Location = new System.Drawing.Point(205, 271);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelScript
            // 
            this.panelScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelScript.Location = new System.Drawing.Point(12, 61);
            this.panelScript.Name = "panelScript";
            this.panelScript.Size = new System.Drawing.Size(451, 205);
            this.panelScript.TabIndex = 5;
            // 
            // FrmScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 302);
            this.Controls.Add(this.panelScript);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LblScript);
            this.Controls.Add(this.TxtNomeScript);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmScript";
            this.Text = "Editar Script";
            this.Load += new System.EventHandler(this.FrmScript_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtNomeScript;
        private System.Windows.Forms.Label LblScript;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel panelScript;
    }
}