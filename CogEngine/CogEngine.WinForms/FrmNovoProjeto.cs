using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CogEngine.Objects;

namespace CogEngine.WinForms
{
    public partial class FrmNovoProjeto : Form
    {
        public string NomeProjeto { get; private set; }
        public string PastaProjeto { get; private set; }

        public FrmNovoProjeto()
        {
            InitializeComponent();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnProcurarPasta_Click(object sender, EventArgs e)
        {
            FolderBrowser.ShowDialog();
            if (!string.IsNullOrEmpty(FolderBrowser.SelectedPath))
            {
                TxtPastaProjeto.Text = FolderBrowser.SelectedPath + '\\' + TxtNomeProjeto.Text;
            }
        }

        private void BtnCriarProjeto_Click(object sender, EventArgs e)
        {
            if (!InformarErro())
            {
                try
                {
                    NomeProjeto = TxtNomeProjeto.Text;
                    PastaProjeto = TxtPastaProjeto.Text;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro ao criar projeto");
                }
            }
        }

        private bool InformarErro()
        {
            bool erro = false;
            ErrProvider.Clear();
            if (TxtNomeProjeto.Text.Trim() == "")
            {
                ErrProvider.SetError(TxtNomeProjeto, "Informe o nome do projeto");
                erro = true;
            }
            if (TxtPastaProjeto.Text.Trim() == "")
            {
                ErrProvider.SetError(BtnProcurarPasta, "Informe uma pasta no disco");
                erro = true;
            }
            return erro;
        }

        private void TxtNomeProjeto_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(FolderBrowser.SelectedPath))
            {
                TxtPastaProjeto.Text = FolderBrowser.SelectedPath + '\\' + TxtNomeProjeto.Text;
            }
        }
    }
}
