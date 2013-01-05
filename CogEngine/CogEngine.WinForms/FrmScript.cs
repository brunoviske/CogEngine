using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CogEngine.WinForms
{
    public partial class FrmScript : Form
    {
        public FrmScript()
        {
            OK = false;
            InitializeComponent();
        }

        public string NomeScript
        {
            get
            {
                return TxtNomeScript.Text;
            }
        }

        public string CodigoScript
        {
            get
            {
                return RTxtCodigoScript.Text;
            }
        }

        public bool OK { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TxtNomeScript.Text.Trim() == "")
            {
                MessageBox.Show("Por favor, informe um nome para seu script");
                return;
            }
            if (RTxtCodigoScript.Text.Trim() == "")
            {
                MessageBox.Show("Por favor, escreva o código do seu script");
                return;
            }
            OK = true;
            Close();
        }

        private void FrmScript_Load(object sender, EventArgs e)
        {
            TxtNomeScript.Text = Script.GetNome();
        }
    }
}
