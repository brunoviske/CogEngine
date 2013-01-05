namespace CogEngine.WinForms
{
    partial class FrmPrincipal
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
            this.menuStripEngine = new System.Windows.Forms.MenuStrip();
            this.arquivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projetoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adicionarCenaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adicionarScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compilarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GrpGameView = new System.Windows.Forms.GroupBox();
            this.BtnAdicionar = new System.Windows.Forms.Button();
            this.LstControles = new System.Windows.Forms.ListBox();
            this.PropertyControl = new System.Windows.Forms.PropertyGrid();
            this.TreeViewObjetos = new System.Windows.Forms.TreeView();
            this.LstScript = new System.Windows.Forms.ListBox();
            this.LblTextoScript = new System.Windows.Forms.Label();
            this.LblOnUpdate = new System.Windows.Forms.Label();
            this.CboUpdate = new System.Windows.Forms.ComboBox();
            this.menuStripEngine.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripEngine
            // 
            this.menuStripEngine.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivoToolStripMenuItem,
            this.projetoToolStripMenuItem});
            this.menuStripEngine.Location = new System.Drawing.Point(0, 0);
            this.menuStripEngine.Name = "menuStripEngine";
            this.menuStripEngine.Size = new System.Drawing.Size(909, 24);
            this.menuStripEngine.TabIndex = 0;
            this.menuStripEngine.Text = "menuStripEngine";
            // 
            // arquivoToolStripMenuItem
            // 
            this.arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            this.arquivoToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.arquivoToolStripMenuItem.Text = "Arquivo";
            // 
            // projetoToolStripMenuItem
            // 
            this.projetoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adicionarCenaToolStripMenuItem,
            this.adicionarScriptToolStripMenuItem,
            this.compilarToolStripMenuItem});
            this.projetoToolStripMenuItem.Name = "projetoToolStripMenuItem";
            this.projetoToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.projetoToolStripMenuItem.Text = "Projeto";
            // 
            // adicionarCenaToolStripMenuItem
            // 
            this.adicionarCenaToolStripMenuItem.Name = "adicionarCenaToolStripMenuItem";
            this.adicionarCenaToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.adicionarCenaToolStripMenuItem.Text = "Adicionar Cena";
            this.adicionarCenaToolStripMenuItem.Click += new System.EventHandler(this.adicionarCenaToolStripMenuItem_Click);
            // 
            // adicionarScriptToolStripMenuItem
            // 
            this.adicionarScriptToolStripMenuItem.Name = "adicionarScriptToolStripMenuItem";
            this.adicionarScriptToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.adicionarScriptToolStripMenuItem.Text = "Adicionar Script";
            this.adicionarScriptToolStripMenuItem.Click += new System.EventHandler(this.adicionarScriptToolStripMenuItem_Click);
            // 
            // compilarToolStripMenuItem
            // 
            this.compilarToolStripMenuItem.Name = "compilarToolStripMenuItem";
            this.compilarToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.compilarToolStripMenuItem.Text = "Compilar";
            this.compilarToolStripMenuItem.Click += new System.EventHandler(this.compilarToolStripMenuItem_Click);
            // 
            // GrpGameView
            // 
            this.GrpGameView.BackColor = System.Drawing.SystemColors.Control;
            this.GrpGameView.Location = new System.Drawing.Point(156, 27);
            this.GrpGameView.Name = "GrpGameView";
            this.GrpGameView.Size = new System.Drawing.Size(425, 384);
            this.GrpGameView.TabIndex = 1;
            this.GrpGameView.TabStop = false;
            // 
            // BtnAdicionar
            // 
            this.BtnAdicionar.Location = new System.Drawing.Point(12, 166);
            this.BtnAdicionar.Name = "BtnAdicionar";
            this.BtnAdicionar.Size = new System.Drawing.Size(120, 23);
            this.BtnAdicionar.TabIndex = 3;
            this.BtnAdicionar.Text = "Adicionar";
            this.BtnAdicionar.UseVisualStyleBackColor = true;
            this.BtnAdicionar.Click += new System.EventHandler(this.BtnAdicionar_Click);
            // 
            // LstControles
            // 
            this.LstControles.FormattingEnabled = true;
            this.LstControles.Location = new System.Drawing.Point(12, 27);
            this.LstControles.Name = "LstControles";
            this.LstControles.Size = new System.Drawing.Size(120, 121);
            this.LstControles.TabIndex = 2;
            // 
            // PropertyControl
            // 
            this.PropertyControl.Location = new System.Drawing.Point(605, 205);
            this.PropertyControl.Name = "PropertyControl";
            this.PropertyControl.Size = new System.Drawing.Size(130, 206);
            this.PropertyControl.TabIndex = 4;
            // 
            // TreeViewObjetos
            // 
            this.TreeViewObjetos.Location = new System.Drawing.Point(605, 27);
            this.TreeViewObjetos.Name = "TreeViewObjetos";
            this.TreeViewObjetos.Size = new System.Drawing.Size(130, 176);
            this.TreeViewObjetos.TabIndex = 5;
            this.TreeViewObjetos.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewObjetos_AfterSelect);
            // 
            // LstScript
            // 
            this.LstScript.FormattingEnabled = true;
            this.LstScript.Location = new System.Drawing.Point(12, 277);
            this.LstScript.Name = "LstScript";
            this.LstScript.Size = new System.Drawing.Size(120, 134);
            this.LstScript.TabIndex = 6;
            // 
            // LblTextoScript
            // 
            this.LblTextoScript.AutoSize = true;
            this.LblTextoScript.Location = new System.Drawing.Point(9, 252);
            this.LblTextoScript.Name = "LblTextoScript";
            this.LblTextoScript.Size = new System.Drawing.Size(98, 13);
            this.LblTextoScript.TabIndex = 0;
            this.LblTextoScript.Text = "Scripts Disponíveis";
            // 
            // LblOnUpdate
            // 
            this.LblOnUpdate.AutoSize = true;
            this.LblOnUpdate.Location = new System.Drawing.Point(762, 27);
            this.LblOnUpdate.Name = "LblOnUpdate";
            this.LblOnUpdate.Size = new System.Drawing.Size(42, 13);
            this.LblOnUpdate.TabIndex = 7;
            this.LblOnUpdate.Text = "Update";
            // 
            // CboUpdate
            // 
            this.CboUpdate.DisplayMember = "NomeScript";
            this.CboUpdate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboUpdate.FormattingEnabled = true;
            this.CboUpdate.Location = new System.Drawing.Point(765, 43);
            this.CboUpdate.Name = "CboUpdate";
            this.CboUpdate.Size = new System.Drawing.Size(121, 21);
            this.CboUpdate.TabIndex = 8;
            this.CboUpdate.ValueMember = "ID";
            this.CboUpdate.SelectedIndexChanged += new System.EventHandler(this.CboUpdate_SelectedIndexChanged);
            // 
            // FrmPrincipal
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 423);
            this.Controls.Add(this.CboUpdate);
            this.Controls.Add(this.LblOnUpdate);
            this.Controls.Add(this.LblTextoScript);
            this.Controls.Add(this.TreeViewObjetos);
            this.Controls.Add(this.LstScript);
            this.Controls.Add(this.PropertyControl);
            this.Controls.Add(this.BtnAdicionar);
            this.Controls.Add(this.GrpGameView);
            this.Controls.Add(this.LstControles);
            this.Controls.Add(this.menuStripEngine);
            this.MainMenuStrip = this.menuStripEngine;
            this.MaximizeBox = false;
            this.Name = "FrmPrincipal";
            this.Text = "CogEngine";
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.menuStripEngine.ResumeLayout(false);
            this.menuStripEngine.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripEngine;
        private System.Windows.Forms.ToolStripMenuItem arquivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projetoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compilarToolStripMenuItem;
        private System.Windows.Forms.GroupBox GrpGameView;
        private System.Windows.Forms.Button BtnAdicionar;
        private System.Windows.Forms.ListBox LstControles;
        private System.Windows.Forms.PropertyGrid PropertyControl;
        private System.Windows.Forms.TreeView TreeViewObjetos;
        private System.Windows.Forms.ToolStripMenuItem adicionarCenaToolStripMenuItem;
        private System.Windows.Forms.ListBox LstScript;
        private System.Windows.Forms.Label LblTextoScript;
        private System.Windows.Forms.ToolStripMenuItem adicionarScriptToolStripMenuItem;
        private System.Windows.Forms.Label LblOnUpdate;
        private System.Windows.Forms.ComboBox CboUpdate;


    }
}