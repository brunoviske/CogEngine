using System;
using System.Drawing;
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
            set
            {
                TxtNomeScript.Text = value;
            }
        }

        public string CodigoScript
        {
            get
            {
                return RTxtCodigoScript.Text;
            }
            set
            {
                RTxtCodigoScript.Text = value;
            }
        }

        public bool OK { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TxtNomeScript.Text.Trim() == "")
            {
                MessageBox.Show("Por favor, informe um nome para seu script.", "CogEngine", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (RTxtCodigoScript.Text.Trim() == "")
            {
                MessageBox.Show("Por favor, escreva o código do seu script.", "CogEngine", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            OK = true;
            Close();
        }

        private void FrmScript_Load(object sender, EventArgs e)
        {
            SetTextFormat();
            TxtNomeScript.Text = Script.GetNome();
        }

        private void SetTextFormat()
        {
            RTxtCodigoScript.Multiline = true;
            RTxtCodigoScript.AcceptsTab = true;
            RTxtCodigoScript.ScrollBars = RichTextBoxScrollBars.ForcedBoth;
            RTxtCodigoScript.SelectionFont = new Font("Courier New", 10, FontStyle.Regular);
            RTxtCodigoScript.SelectionColor = Color.Black;
        }
    }
}
