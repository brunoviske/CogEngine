using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CogEngine.Objects;

namespace CogEngine.WinForms
{
    public partial class FrmInicio : Form
    {
        FrmPrincipal _Principal;
        private bool _FecharFormPrincipal = true;
        private bool _NovoProjeto;

        public FrmInicio(FrmPrincipal principal, bool novoProjeto)
        {
            _Principal = principal;
            InitializeComponent();
            _NovoProjeto = novoProjeto;
        }

        private void NovoProjeto()
        {
            FrmNovoProjeto frmNovoProjeto = new FrmNovoProjeto();
            frmNovoProjeto.ShowDialog();
            if (frmNovoProjeto.PastaProjeto != null && frmNovoProjeto.NomeProjeto != null)
            {
                _Principal.ProjetoJogo = Jogo.CriarProjeto(frmNovoProjeto.PastaProjeto, frmNovoProjeto.NomeProjeto);
                _FecharFormPrincipal = false;
                Close();
            }
        }

        private void BtnNovoProjeto_Click(object sender, EventArgs e)
        {
            NovoProjeto();
        }

        private void FrmInicio_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_FecharFormPrincipal)
            {
                _Principal.Close();
            }
        }

        private void BtnAbrirProjeto_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrirProjeto = new OpenFileDialog();
            abrirProjeto.Filter = "Projeto CogEngine|*" + Jogo.EXTENSAO_PROJETO;
            abrirProjeto.ShowDialog();
            string caminhoArquivo = abrirProjeto.FileName;
            if (!string.IsNullOrEmpty(caminhoArquivo.Trim()))
            {
                _Principal.ProjetoJogo = Jogo.AbrirProjeto(caminhoArquivo);
                _FecharFormPrincipal = false;
                Close();
            }
        }

        private void FrmInicio_Load(object sender, EventArgs e)
        {
            if (_NovoProjeto) NovoProjeto();
        }
    }
}
