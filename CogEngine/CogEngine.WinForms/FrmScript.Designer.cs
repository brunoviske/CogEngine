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
            this.RTxtCodigoScript = new System.Windows.Forms.RichTextBox();
            this.TxtNomeScript = new System.Windows.Forms.TextBox();
            this.LblScript = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RTxtCodigoScript
            // 
            this.RTxtCodigoScript.Location = new System.Drawing.Point(13, 67);
            this.RTxtCodigoScript.Name = "RTxtCodigoScript";
            this.RTxtCodigoScript.Size = new System.Drawing.Size(267, 194);
            this.RTxtCodigoScript.TabIndex = 0;
            this.RTxtCodigoScript.Text = "";
            // 
            // TxtNomeScript
            // 
            this.TxtNomeScript.Location = new System.Drawing.Point(111, 12);
            this.TxtNomeScript.Name = "TxtNomeScript";
            this.TxtNomeScript.Size = new System.Drawing.Size(169, 20);
            this.TxtNomeScript.TabIndex = 1;
            // 
            // LblScript
            // 
            this.LblScript.AutoSize = true;
            this.LblScript.Location = new System.Drawing.Point(13, 16);
            this.LblScript.Name = "LblScript";
            this.LblScript.Size = new System.Drawing.Size(80, 13);
            this.LblScript.TabIndex = 2;
            this.LblScript.Text = "Nome do Script";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Código";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(205, 270);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 305);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LblScript);
            this.Controls.Add(this.TxtNomeScript);
            this.Controls.Add(this.RTxtCodigoScript);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmScript";
            this.Text = "Novo Script";
            this.Load += new System.EventHandler(this.FrmScript_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox RTxtCodigoScript;
        private System.Windows.Forms.TextBox TxtNomeScript;
        private System.Windows.Forms.Label LblScript;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}