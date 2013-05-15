using System;
using System.Drawing;
using System.Windows.Forms;
using CogEngine.ScriptEditor;
using CogEngine.Objects;

namespace CogEngine.WinForms
{
    public partial class FrmScript : Form
    {
        FastColoredTextBox TxtCodigoScript = new FastColoredTextBox();

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

        public string CodigoScript { get; set; }

        public bool OK { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TxtNomeScript.Text.Trim() == "")
            {
                MessageBox.Show("Por favor, informe um nome para seu script.", "CogEngine", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (TxtCodigoScript.Text.Trim() == "")
            {
                MessageBox.Show("Por favor, escreva o código do seu script.", "CogEngine", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            NomeScript = TxtNomeScript.Text;
            CodigoScript = TxtCodigoScript.Text;

            OK = true;
            Close();
        }

        private void FrmScript_Load(object sender, EventArgs e)
        {
            SetTextFormat();
            LoadContent();
        }

        private void SetTextFormat()
        {
            TxtCodigoScript.Font = new Font("Consolas", 9.75f);
            TxtCodigoScript.Dock = DockStyle.Fill;
            TxtCodigoScript.BorderStyle = BorderStyle.Fixed3D;
            TxtCodigoScript.VirtualSpace = true;
            TxtCodigoScript.LeftPadding = 17;
            TxtCodigoScript.Language = Language.CSharp;
            TxtCodigoScript.AddStyle(new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Gray))));//same words style
            TxtCodigoScript.IsChanged = false;
            TxtCodigoScript.HighlightingRangeType = HighlightingRangeType.VisibleRange;
            panelScript.Controls.Add(TxtCodigoScript);
        }

        private void LoadContent()
        {
            if (string.IsNullOrEmpty(NomeScript) || string.IsNullOrWhiteSpace(NomeScript))
                TxtNomeScript.Text = Script.GetNome();
            else
                TxtNomeScript.Text = NomeScript;

            if (string.IsNullOrEmpty(CodigoScript) || string.IsNullOrWhiteSpace(CodigoScript))
                TxtCodigoScript.Text = string.Empty;
            else
                TxtCodigoScript.Text = CodigoScript;

            TxtCodigoScript.Focus();
        }
    }
}
