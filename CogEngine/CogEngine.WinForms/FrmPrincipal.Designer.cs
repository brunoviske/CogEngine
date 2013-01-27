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
            this.novoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salvarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fecharToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projetoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adicionarCenaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adicionarScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compilarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GrpGameView = new System.Windows.Forms.GroupBox();
            this.LstControles = new System.Windows.Forms.ListBox();
            this.PropertyControl = new System.Windows.Forms.PropertyGrid();
            this.TreeViewObjetos = new System.Windows.Forms.TreeView();
            this.LstScript = new System.Windows.Forms.ListBox();
            this.LblTextoScript = new System.Windows.Forms.Label();
            this.LblOnUpdate = new System.Windows.Forms.Label();
            this.CboUpdate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMinimizar = new System.Windows.Forms.PictureBox();
            this.btnFechar = new System.Windows.Forms.PictureBox();
            this.btnMaximizar = new System.Windows.Forms.PictureBox();
            this.menuStripEngine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFechar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximizar)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStripEngine
            // 
            this.menuStripEngine.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStripEngine.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivoToolStripMenuItem,
            this.projetoToolStripMenuItem});
            this.menuStripEngine.Location = new System.Drawing.Point(12, 26);
            this.menuStripEngine.Name = "menuStripEngine";
            this.menuStripEngine.Size = new System.Drawing.Size(126, 24);
            this.menuStripEngine.TabIndex = 0;
            this.menuStripEngine.Text = "menuStripEngine";
            // 
            // arquivoToolStripMenuItem
            // 
            this.arquivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.novoToolStripMenuItem,
            this.abrirToolStripMenuItem,
            this.salvarToolStripMenuItem,
            this.fecharToolStripMenuItem,
            this.sairToolStripMenuItem});
            this.arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            this.arquivoToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.arquivoToolStripMenuItem.Text = "Arquivo";
            // 
            // novoToolStripMenuItem
            // 
            this.novoToolStripMenuItem.Name = "novoToolStripMenuItem";
            this.novoToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.novoToolStripMenuItem.Text = "Novo";
            this.novoToolStripMenuItem.Click += new System.EventHandler(this.novoToolStripMenuItem_Click);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // salvarToolStripMenuItem
            // 
            this.salvarToolStripMenuItem.Name = "salvarToolStripMenuItem";
            this.salvarToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.salvarToolStripMenuItem.Text = "Salvar";
            this.salvarToolStripMenuItem.Click += new System.EventHandler(this.salvarToolStripMenuItem_Click);
            // 
            // fecharToolStripMenuItem
            // 
            this.fecharToolStripMenuItem.Name = "fecharToolStripMenuItem";
            this.fecharToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.fecharToolStripMenuItem.Text = "Fechar";
            this.fecharToolStripMenuItem.Click += new System.EventHandler(this.fecharToolStripMenuItem_Click);
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.sairToolStripMenuItem.Text = "Sair";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.sairToolStripMenuItem_Click);
            // 
            // projetoToolStripMenuItem
            // 
            this.projetoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adicionarCenaToolStripMenuItem,
            this.adicionarScriptToolStripMenuItem,
            this.compilarToolStripMenuItem});
            this.projetoToolStripMenuItem.Name = "projetoToolStripMenuItem";
            this.projetoToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.projetoToolStripMenuItem.Text = "Projeto";
            // 
            // adicionarCenaToolStripMenuItem
            // 
            this.adicionarCenaToolStripMenuItem.Name = "adicionarCenaToolStripMenuItem";
            this.adicionarCenaToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.adicionarCenaToolStripMenuItem.Text = "Adicionar Cena";
            this.adicionarCenaToolStripMenuItem.Click += new System.EventHandler(this.adicionarCenaToolStripMenuItem_Click);
            // 
            // adicionarScriptToolStripMenuItem
            // 
            this.adicionarScriptToolStripMenuItem.Name = "adicionarScriptToolStripMenuItem";
            this.adicionarScriptToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.adicionarScriptToolStripMenuItem.Text = "Adicionar Script";
            this.adicionarScriptToolStripMenuItem.Click += new System.EventHandler(this.adicionarScriptToolStripMenuItem_Click);
            // 
            // compilarToolStripMenuItem
            // 
            this.compilarToolStripMenuItem.Name = "compilarToolStripMenuItem";
            this.compilarToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.compilarToolStripMenuItem.Text = "Compilar";
            this.compilarToolStripMenuItem.Click += new System.EventHandler(this.compilarToolStripMenuItem_Click);
            // 
            // GrpGameView
            // 
            this.GrpGameView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpGameView.BackColor = System.Drawing.SystemColors.Control;
            this.GrpGameView.Location = new System.Drawing.Point(156, 69);
            this.GrpGameView.Name = "GrpGameView";
            this.GrpGameView.Size = new System.Drawing.Size(800, 480);
            this.GrpGameView.TabIndex = 1;
            this.GrpGameView.TabStop = false;
            // 
            // LstControles
            // 
            this.LstControles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LstControles.FormattingEnabled = true;
            this.LstControles.Location = new System.Drawing.Point(12, 85);
            this.LstControles.Name = "LstControles";
            this.LstControles.Size = new System.Drawing.Size(138, 225);
            this.LstControles.TabIndex = 2;
            this.LstControles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LstControles_MouseDoubleClick);
            // 
            // PropertyControl
            // 
            this.PropertyControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PropertyControl.Location = new System.Drawing.Point(962, 346);
            this.PropertyControl.Name = "PropertyControl";
            this.PropertyControl.Size = new System.Drawing.Size(200, 206);
            this.PropertyControl.TabIndex = 4;
            // 
            // TreeViewObjetos
            // 
            this.TreeViewObjetos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeViewObjetos.Location = new System.Drawing.Point(962, 115);
            this.TreeViewObjetos.Name = "TreeViewObjetos";
            this.TreeViewObjetos.Size = new System.Drawing.Size(200, 195);
            this.TreeViewObjetos.TabIndex = 5;
            this.TreeViewObjetos.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewObjetos_AfterSelect);
            // 
            // LstScript
            // 
            this.LstScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LstScript.FormattingEnabled = true;
            this.LstScript.Location = new System.Drawing.Point(12, 340);
            this.LstScript.Name = "LstScript";
            this.LstScript.Size = new System.Drawing.Size(138, 212);
            this.LstScript.TabIndex = 6;
            // 
            // LblTextoScript
            // 
            this.LblTextoScript.AutoSize = true;
            this.LblTextoScript.Location = new System.Drawing.Point(9, 324);
            this.LblTextoScript.Name = "LblTextoScript";
            this.LblTextoScript.Size = new System.Drawing.Size(98, 13);
            this.LblTextoScript.TabIndex = 0;
            this.LblTextoScript.Text = "Scripts Disponíveis";
            // 
            // LblOnUpdate
            // 
            this.LblOnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblOnUpdate.AutoSize = true;
            this.LblOnUpdate.Location = new System.Drawing.Point(959, 72);
            this.LblOnUpdate.Name = "LblOnUpdate";
            this.LblOnUpdate.Size = new System.Drawing.Size(42, 13);
            this.LblOnUpdate.TabIndex = 7;
            this.LblOnUpdate.Text = "Update";
            // 
            // CboUpdate
            // 
            this.CboUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CboUpdate.DisplayMember = "NomeScript";
            this.CboUpdate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboUpdate.FormattingEnabled = true;
            this.CboUpdate.Location = new System.Drawing.Point(962, 88);
            this.CboUpdate.Name = "CboUpdate";
            this.CboUpdate.Size = new System.Drawing.Size(200, 21);
            this.CboUpdate.TabIndex = 8;
            this.CboUpdate.ValueMember = "ID";
            this.CboUpdate.SelectedIndexChanged += new System.EventHandler(this.CboUpdate_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Componentes";
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimizar.Image = global::CogEngine.WinForms.Properties.Resources.Minimizar;
            this.btnMinimizar.Location = new System.Drawing.Point(1076, 15);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(22, 10);
            this.btnMinimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMinimizar.TabIndex = 12;
            this.btnMinimizar.TabStop = false;
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFechar.Image = global::CogEngine.WinForms.Properties.Resources.Fechar1;
            this.btnFechar.Location = new System.Drawing.Point(1140, 12);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(22, 16);
            this.btnFechar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnFechar.TabIndex = 11;
            this.btnFechar.TabStop = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // btnMaximizar
            // 
            this.btnMaximizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximizar.Image = global::CogEngine.WinForms.Properties.Resources.Maximizar3;
            this.btnMaximizar.Location = new System.Drawing.Point(1111, 12);
            this.btnMaximizar.Name = "btnMaximizar";
            this.btnMaximizar.Size = new System.Drawing.Size(16, 16);
            this.btnMaximizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMaximizar.TabIndex = 10;
            this.btnMaximizar.TabStop = false;
            this.btnMaximizar.Visible = false;
            this.btnMaximizar.Click += new System.EventHandler(this.btnMaximizar_Click);
            // 
            // FrmPrincipal
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1185, 600);
            this.Controls.Add(this.btnMinimizar);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.btnMaximizar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CboUpdate);
            this.Controls.Add(this.LblOnUpdate);
            this.Controls.Add(this.LblTextoScript);
            this.Controls.Add(this.TreeViewObjetos);
            this.Controls.Add(this.LstScript);
            this.Controls.Add(this.PropertyControl);
            this.Controls.Add(this.GrpGameView);
            this.Controls.Add(this.LstControles);
            this.Controls.Add(this.menuStripEngine);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStripEngine;
            this.MinimizeBox = false;
            this.Name = "FrmPrincipal";
            this.Text = "CogEngine";
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmPrincipal_MouseDown);
            this.menuStripEngine.ResumeLayout(false);
            this.menuStripEngine.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFechar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximizar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripEngine;
        private System.Windows.Forms.ToolStripMenuItem arquivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projetoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compilarToolStripMenuItem;
        private System.Windows.Forms.GroupBox GrpGameView;
        private System.Windows.Forms.ListBox LstControles;
        private System.Windows.Forms.PropertyGrid PropertyControl;
        private System.Windows.Forms.TreeView TreeViewObjetos;
        private System.Windows.Forms.ToolStripMenuItem adicionarCenaToolStripMenuItem;
        private System.Windows.Forms.ListBox LstScript;
        private System.Windows.Forms.Label LblTextoScript;
        private System.Windows.Forms.ToolStripMenuItem adicionarScriptToolStripMenuItem;
        private System.Windows.Forms.Label LblOnUpdate;
        private System.Windows.Forms.ComboBox CboUpdate;
        private System.Windows.Forms.ToolStripMenuItem novoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fecharToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salvarToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox btnMaximizar;
        private System.Windows.Forms.PictureBox btnFechar;
        private System.Windows.Forms.PictureBox btnMinimizar;


    }
}