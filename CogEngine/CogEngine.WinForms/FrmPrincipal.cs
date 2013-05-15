using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using CogEngine.Objects;
using CogEngine.Objects.WinForms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace CogEngine.WinForms
{
    public partial class FrmPrincipal : Form
    {
        #region Propriedades
        private Jogo _Projeto;
        public Jogo ProjetoJogo
        {
            get
            {
                return _Projeto;
            }
            set
            {
                _Projeto = value;
                if (_Projeto != null)
                {
                    TreeViewObjetos.Nodes.Clear();
                    Cena[] cenaArray = _Projeto.ListarCena();
                    foreach (CenaWinForm cena in cenaArray)
                    {
                        cena.Painel.Size = GrpGameView.Size;
                        cena.OnNomeChanged += OnNomeChanged;
                        TreeViewObjetos.Nodes.Add(cena.ID, cena.Nome);
                        foreach (ConcentradorObjeto o in cena.ListarObjetos())
                        {
                            //Crio o controle, adiciono seu evento para exibição dos detalhes e os adiciono na cena                       
                            Control ctr = o.WinControl.GetControl();
                            ctr.Click += ControClick;
                            ctr.MouseDown += new MouseEventHandler(ControMouseDown);
                            ctr.MouseUp += new MouseEventHandler(ControMouseUp);
                            ctr.MouseMove += new MouseEventHandler(ControMouseMove);

                            o.PropriedadeInvalida += OnPropriedadeInvalida;
                            o.NomeAlterado += OnNomeChanged;
                            TreeViewObjetos.Nodes[cena.ID].Nodes.Add(o.ID, o.Nome);
                        }
                        cena.AlteracaoObjeto += ListaAlterada;
                    }
                    CboUpdate.Items.Clear();
                    LstScript.Items.Clear();
                    CboUpdate.Items.Add(VAZIO);
                    foreach (Script s in _Projeto.ListarScript())
                    {
                        CboUpdate.Items.Add(s);
                        LstScript.Items.Add(s);
                    }
                    CboUpdate.SelectedIndex = 0;

                    LstSons.Items.Clear();
                    foreach (Som s in _Projeto.ListarSom())
                    {
                        LstSons.Items.Add(s);
                    }

                    //Carrego a primeira cena caso haja alguma
                    if (cenaArray.Length > 0)
                    {
                        CarregarCena((CenaWinForm)cenaArray[0]);
                    }
                    _Projeto.AlteracaoLista += ListaAlterada;
                }
            }
        }
        CenaWinForm _CenaAtual;
        ToolTip toolMaxRest = new ToolTip();
        public bool IsDragging { get; set; }
        public Control ControleTela { get; set; }
        public Int64 MouseXInicial { get; set; }
        public Int64 MouseYInicial { get; set; }

        #region Propriedades do Drag n Drop dos itens
        Control controleTela = null;
        bool redimensionandoControle = false;
        int resizingMargin = 5;
        private Point pontoInicialDrag;
        private Size tamanhoInicial;
        Rectangle novoRetangulo = Rectangle.Empty;
        #endregion
        #endregion

        #region Constantes
        const string VAZIO = "(Vazio)";
        #endregion

        #region Variáveis para arredondamento das bordas
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
        int nLeftRect, // x-coordenada da quina superior esquerda
        int nTopRect, // y-coordenada da quina superior esquerda
        int nRightRect, // x-coordenada da quina inferior direita
        int nBottomRect, // y-coordenada da quina inferior direita
        int nWidthEllipse, // largura da elipse
        int nHeightEllipse // altura da ellipse
        );
        #endregion

        #region Controle Drag/Drop do form
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void FrmPrincipal_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion

        #region Drag n Drop Controles
        private void DoDrag(System.Windows.Forms.Control controle)
        {
            GetMouseInitialPosition();
            ControleTela = controle;
        }

        private void DoDrop(System.Windows.Forms.Control controle)
        {
            if (System.Windows.Forms.Cursor.Position.X > MouseXInicial)
                ControleTela.Left += System.Windows.Forms.Cursor.Position.X - int.Parse(MouseXInicial.ToString());
            else
                ControleTela.Left -= int.Parse(MouseXInicial.ToString()) - System.Windows.Forms.Cursor.Position.X;

            if (System.Windows.Forms.Cursor.Position.Y > MouseYInicial)
                ControleTela.Top += System.Windows.Forms.Cursor.Position.Y - int.Parse(MouseYInicial.ToString());
            else
                ControleTela.Top -= int.Parse(MouseYInicial.ToString()) - System.Windows.Forms.Cursor.Position.Y;

            ControleTela = null;
        }

        private void GetMouseInitialPosition()
        {
            MouseXInicial = System.Windows.Forms.Cursor.Position.X;
            MouseYInicial = System.Windows.Forms.Cursor.Position.Y;
        }
        #endregion

        public new System.Windows.Forms.Control.ControlCollection Controls
        {
            get
            {
                return base.Controls;
            }
        }

        public FrmPrincipal()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width - 10, Height - 10, 20, 20));
            SetToolTip();
            LoadItems();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            ClearEngine(false);
        }

        int _NumCena = 1;
        private CenaWinForm AdicionarCena()
        {
            return AdicionarCena("cena " + _NumCena++);
        }

        private CenaWinForm AdicionarCena(string nome)
        {
            CenaWinForm cena = new CenaWinForm();
            cena.Nome = nome;
            cena.Painel.Size = GrpGameView.Size;
            cena.OnNomeChanged += OnNomeChanged;
            cena.AlteracaoObjeto += ListaAlterada;
            ProjetoJogo.AdicionarCena(cena);
            CarregarCena(cena);
            return cena;
        }

        private void OnNomeChanged(object sender, NomeChangedEvent e)
        {
            try
            {
                TreeViewObjetos.Nodes.Find(e.ID, true)[0].Text = e.NomeNovo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Falha ao atribuir o nome ao componente. Detalhes: {0}", ex.Message), "CogEngine - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnPropriedadeInvalida(object sender, PropriedadeInvalidaEvent e)
        {
            MessageBox.Show(e.MensagemErro, "Erro ao alterar " + e.NomePropriedade);
        }

        private void CarregarCena(CenaWinForm cena)
        {
            GrpGameView.Controls.Clear();
            GrpGameView.Controls.Add(cena.Painel);
            _CenaAtual = cena;
        }

        public void ControClick(object sender, EventArgs e)
        {
            if (sender is ICogEngineWinControl)
            {
                ICogEngineWinControl winControl = (ICogEngineWinControl)sender;
                CarregarDetalhe(winControl);
                CboUpdate.SelectedValue = winControl.IDScript;
            }
            else
            {
                MessageBox.Show("O tipo do objeto para a operação não era o esperado", "CogEngine - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void compilarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.ShowDialog();
                if (!string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    ProjetoJogo.Compilar(dialog.SelectedPath);
                    MessageBox.Show("Seu projeto foi compilado com sucesso!", "CogEngine - compilação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao compilar projeto: " + ex.Message, "Erro - compilação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Script RetornarScript(string idScript)
        {
            Script s;
            foreach (object item in CboUpdate.Items)
            {
                if (item is Script)
                {
                    s = (Script)item;
                    if (s.ID == idScript)
                    {
                        return s;
                    }
                }
            }
            return null;
        }

        private void adicionarCenaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdicionarCena();
        }

        private void adicionarScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmScript frmScript = new FrmScript())
            {
                frmScript.ShowDialog();
                if (frmScript.OK)
                {
                    Script script = new Script();
                    script.NomeAmigavel = frmScript.NomeScript;
                    script.CodigoScript = frmScript.CodigoScript;
                    ProjetoJogo.AdicionarScript(script);
                }
            }
        }

        private void adicionarSomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Procurar som";
            openFileDialog1.Filter = "Sons (*.WAV)|*.WAV";
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            DialogResult resultado = openFileDialog1.ShowDialog();

            if (resultado == System.Windows.Forms.DialogResult.OK)
            {
                if (openFileDialog1.FileName.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
                {
                    if (!LstSons.Items.Contains(openFileDialog1.SafeFileName))
                    {
                        SomWinForm som = new SomWinForm(ProjetoJogo);
                        som.CopiarArquivo(openFileDialog1.FileName);
                        int i = openFileDialog1.FileName.LastIndexOf('\\');
                        som.Nome = openFileDialog1.FileName.Substring(i + 1);
                        ProjetoJogo.AdicionarSom(som);
                    }
                    else
                    {
                        MessageBox.Show("Esse arquivo já está incluso na lista de sons!");
                    }
                }
                else
                {
                    MessageBox.Show("É permitido apenas arquivo do tipo .wav");
                }
            }
        }

        private void ExcluirArquivo(string pasta)
        {
            string[] arquivos = Directory.GetFiles(pasta);
            foreach (string s in arquivos)
            {
                File.Delete(s);
            }
        }

        #region ToolBox

        private void LoadItems()
        {
            Type t = typeof(ObjetoAttribute);
            IEnumerable<Type> types = System.Reflection.Assembly.GetAssembly(t).GetTypes().Where(type => type.GetCustomAttributes(t, false).Length > 0);
            ObjetoAttribute o;
            foreach (Type type in types)
            {
                if (!type.Equals(t))
                {
                    o = (ObjetoAttribute)type.GetCustomAttributes(t, false)[0];
                    LstControles.Items.Add(o);
                }
            }
        }

        private void LstControles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (LstControles.SelectedItem != null)
            {
                ObjetoAttribute o = (ObjetoAttribute)LstControles.SelectedItem;
                ConcentradorObjeto objeto = (ConcentradorObjeto)o.TipoRelacionado.GetConstructor(new Type[] { typeof(Jogo) }).Invoke(new object[] { ProjetoJogo });
                objeto.IniciarNome();
                if (objeto.WinControl != null)
                {
                    Control c = objeto.WinControl.InitWinControl();

                    objeto.PropriedadeInvalida += OnPropriedadeInvalida;
                    objeto.NomeAlterado += OnNomeChanged;
                    c.Click += ControClick;
                    c.MouseDown += new MouseEventHandler(ControMouseDown);
                    c.MouseUp += new MouseEventHandler(ControMouseUp);
                    c.MouseMove += new MouseEventHandler(ControMouseMove);

                    _CenaAtual.AdicionarObjeto(objeto);
                }
                else
                {
                    MessageBox.Show("O componente para renderização não foi inicializado.", "CogEngine - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void ControMouseMove(object sender, MouseEventArgs e)
        {
            if (controleTela != null)
            {
                if (redimensionandoControle)
                {
                    // erase rect
                    if (novoRetangulo.Width > 0 && novoRetangulo.Height > 0)
                        ControlPaint.DrawReversibleFrame(novoRetangulo, this.ForeColor, FrameStyle.Dashed);
                    // calculate rect new size
                    novoRetangulo.Width = e.X - this.pontoInicialDrag.X + this.tamanhoInicial.Width;
                    novoRetangulo.Height = e.Y - this.pontoInicialDrag.Y + this.tamanhoInicial.Height;
                    // draw rect
                    if (novoRetangulo.Width > 0 && novoRetangulo.Height > 0)
                        ControlPaint.DrawReversibleFrame(novoRetangulo, this.ForeColor, FrameStyle.Dashed);
                }
                else
                {
                    Point pt;
                    if (controleTela == sender)
                        pt = new Point(e.X, e.Y);
                    else
                        pt = controleTela.PointToClient((sender as Control).PointToScreen(new Point(e.X, e.Y)));

                    controleTela.Left += pt.X - this.pontoInicialDrag.X;
                    controleTela.Top += pt.Y - this.pontoInicialDrag.Y;
                }
            }
        }

        public void ControMouseUp(object sender, MouseEventArgs e)
        {
            if (redimensionandoControle)
            {
                if (novoRetangulo.Width > 0 && novoRetangulo.Height > 0)
                {
                    // apago o retangulo
                    ControlPaint.DrawReversibleFrame(novoRetangulo, this.ForeColor, FrameStyle.Dashed);
                }
                // compara a largura minima e o tamanho
                if (novoRetangulo.Width > 10 && novoRetangulo.Height > 10)
                {
                    // set size 
                    this.controleTela.Size = novoRetangulo.Size;
                }
                else
                {
                    // you might want to set some minimal size here
                    this.controleTela.Size = new Size((int)Math.Max(10, novoRetangulo.Width), Math.Max(10, novoRetangulo.Height));
                }
            }

            this.controleTela = null;
            this.pontoInicialDrag = Point.Empty;
            this.Cursor = Cursors.Default;

            CarregarDetalhe((Control)sender);
        }

        public void ControMouseDown(object sender, MouseEventArgs e)
        {
            controleTela = sender as Control;
            if (controleTela.GetType() != typeof(TextoWinControl))
            {
                if ((e.X <= resizingMargin) || (e.X >= controleTela.Width - resizingMargin) ||
                    (e.Y <= resizingMargin) || (e.Y >= controleTela.Height - resizingMargin))
                {
                    redimensionandoControle = true;

                    // indicate resizing
                    this.Cursor = Cursors.SizeNWSE;

                    // tamanho inicial
                    this.tamanhoInicial = new Size(e.X, e.Y);

                    //Ajuste do ponto
                    Point p = new Point(controleTela.Location.X + GrpGameView.Location.X, controleTela.Location.Y + GrpGameView.Location.Y);

                    // Obtenho a localização do picture box
                    Point pt = this.PointToScreen(p);
                    novoRetangulo = new Rectangle(pt, tamanhoInicial);

                    // desenho o retangulo
                    ControlPaint.DrawReversibleFrame(novoRetangulo, this.ForeColor, FrameStyle.Dashed);
                }
                else
                {
                    redimensionandoControle = false;
                    // indicate moving
                    this.Cursor = Cursors.SizeAll;
                }

                // start point location
                this.pontoInicialDrag = e.Location;
            }
            else
            {
                this.Cursor = Cursors.SizeAll;
                // start point location
                this.pontoInicialDrag = e.Location;
            }
        }

        #endregion

        #region detalhes

        public void CarregarDetalhe(object objeto)
        {
            PropertyControl.SelectedObject = objeto;
            PropertyControl.BrowsableAttributes = new AttributeCollection(
                new Attribute[]{
                    new CategoryAttribute("Misc"),
                    new BrowsableAttribute(true)
                }
            );
            if (objeto is ICogEngineWinControl)
            {
                ICogEngineWinControl winControl = (ICogEngineWinControl)objeto;
                if (winControl.IDScript == null)
                {
                    CboUpdate.SelectedItem = VAZIO;
                }
                else
                {
                    CboUpdate.SelectedItem = RetornarScript(winControl.IDScript);
                }
            }
        }

        #endregion

        private void TreeViewObjetos_AfterSelect(object sender, TreeViewEventArgs e)
        {
            CenaWinForm cena;
            if (e.Node.Parent != null)
            {
                cena = (CenaWinForm)ProjetoJogo.ListarCena().First(c => c.Nome == e.Node.Parent.Text);
                ConcentradorObjeto objeto = cena.ListarObjetos().First(f => f.Nome == e.Node.Text);
                CarregarDetalhe(objeto.WinControl);
            }
            else
            {
                cena = (CenaWinForm)ProjetoJogo.ListarCena().First(c => c.Nome == e.Node.Text);
                CarregarCena(cena);
                CarregarDetalhe(cena);
            }
        }

        private void TreeViewObjetos_KeyDown(object sender, KeyEventArgs e)
        {
            CenaWinForm cena;
            if (e.KeyCode == Keys.Delete)
            {
                if (TreeViewObjetos.SelectedNode != null)
                {
                    if (DialogResult.Yes == MessageBox.Show("Confirma a exclusão deste item?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        if (TreeViewObjetos.SelectedNode.Parent != null)
                        {
                            cena = (CenaWinForm)ProjetoJogo.ListarCena().First(c => c.Nome == TreeViewObjetos.SelectedNode.Parent.Text);
                            ConcentradorObjeto objeto = cena.ListarObjetos().First(f => f.Nome == TreeViewObjetos.SelectedNode.Text);
                            cena.RemoverObjeto(objeto);
                        }
                        else
                        {
                            cena = (CenaWinForm)ProjetoJogo.ListarCena().First(c => c.Nome == TreeViewObjetos.SelectedNode.Text);
                            ProjetoJogo.RemoverCena(cena);
                        }
                    }
                }
            }
        }

        private void CboUpdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PropertyControl.SelectedObject is ICogEngineWinControl)
            {
                ICogEngineWinControl control = (ICogEngineWinControl)PropertyControl.SelectedObject;
                if (CboUpdate.SelectedItem is Script)
                {
                    control.IDScript = ((Script)CboUpdate.SelectedItem).ID;
                }
                else
                {
                    control.IDScript = null;
                }
            }
        }

        #region arquivo
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Projeto CogEngine|*" + Jogo.EXTENSAO_PROJETO;
            op.Title = "CogEngine - Abrir";
            op.ShowDialog();
            string caminhoArquivo = op.FileName;
            if (!string.IsNullOrEmpty(caminhoArquivo.Trim()))
            {
                ProjetoJogo = Jogo.AbrirProjeto(caminhoArquivo);
            }
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ProjetoJogo.Salvar();
                MessageBox.Show("Seu projeto foi salvo com sucesso!", "CogEngine", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Falha ao salvar projeto!", "CogEngine - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void fecharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja fechar o projeto?", "CogEngine - Fechar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                ClearEngine(false);
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FecharEngine();
        }

        private void FecharEngine()
        {
            if (MessageBox.Show("Deseja encerrar a engine?", "CogEngine - Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Close();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearEngine(true);
        }
        #endregion

        private void SetToolTip()
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btnFechar, "Fechar");
            tt.SetToolTip(btnMinimizar, "Minimizar");

            toolMaxRest.ShowAlways = true;
        }

        private void ClearEngine(bool novoProjeto)
        {
            ProjetoJogo = null;
            TreeViewObjetos.Nodes.Clear();
            GrpGameView.Controls.Clear();
            CboUpdate.Items.Clear();
            LstScript.Items.Clear();
            PropertyControl.SelectedObject = null;
            _NumCena = 1;

            Visible = false;
            FrmInicio inicio = new FrmInicio(this, novoProjeto);
            inicio.ShowDialog();
            if (!IsDisposed)
            {
                Visible = true;
            }
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            toolMaxRest.RemoveAll();

            if (this.WindowState != FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
                toolMaxRest.SetToolTip(btnMaximizar, "Restaurar");
                Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width - 0, Height - 0, 0, 0));
                btnMaximizar.Image = Image.FromFile(@"Resources\Restaurar.png");
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width - 10, Height - 10, 20, 20));
                toolMaxRest.SetToolTip(btnMaximizar, "Maximizar");
                btnMaximizar.Image = Image.FromFile(@"Resources\Maximizar.png");
            }

            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            FecharEngine();
        }

        private void LstScript_DoubleClick(object sender, EventArgs e)
        {
            //Obtenho o script para edição
            if (LstScript.SelectedItem != null)
            {
                Script s = (Script)LstScript.SelectedItem;

                FrmScript frmScript = new FrmScript();
                frmScript.CodigoScript = s.CodigoScript;
                frmScript.NomeScript = s.NomeAmigavel;
                frmScript.ShowDialog();
                if (frmScript.OK)
                {
                    //Atribuo as alterações
                    s.CodigoScript = frmScript.CodigoScript;
                    s.NomeAmigavel = frmScript.NomeScript;

                    LstScript.Items[LstScript.Items.IndexOf(s)] = s;
                    CboUpdate.Items[CboUpdate.Items.IndexOf(s)] = s;
                }
            }
        }

        private void LstScript_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (LstScript.SelectedItem != null)
                {
                    object objLista = LstScript.SelectedItem;

                    if (DialogResult.Yes == MessageBox.Show("Confirma a exclusão deste script?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        ProjetoJogo.RemoverScript((Script)objLista);
                    }
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (LstScript.SelectedItem != null)
                {
                    FrmScript frmScript = new FrmScript();
                    frmScript.CodigoScript = ((Script)LstScript.SelectedItem).CodigoScript;
                    frmScript.NomeScript = ((Script)LstScript.SelectedItem).NomeAmigavel;
                    frmScript.ShowDialog();

                    //Atribuo as alterações
                    ((Script)LstScript.SelectedItem).CodigoScript = frmScript.CodigoScript;
                    ((Script)LstScript.SelectedItem).NomeAmigavel = frmScript.NomeScript;
                }
            }
        }

        private void PropertyControl_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "ZIndex")
            {
                _CenaAtual.Ordenar();
                _CenaAtual.CarregarPainel();
            }
        }

        public void ListaAlterada(object sender, AlteracaoListaEventArgs e)
        {
            if (e.TipoAlteracao == TipoAlteracaoLista.Adicao)
            {
                if (e.Objeto is CenaWinForm)
                {
                    CenaWinForm c = (CenaWinForm)e.Objeto;
                    TreeViewObjetos.Nodes.Add(c.ID, c.Nome);
                }
                else if (e.Objeto is Script)
                {
                    Script s = (Script)e.Objeto;
                    LstScript.Items.Add(s);
                    CboUpdate.Items.Add(s);
                }
                else if (e.Objeto is Som)
                {
                    Som s = (Som)e.Objeto;
                    LstSons.Items.Add(s);
                }
                else if (e.Objeto is ConcentradorObjeto)
                {
                    CenaWinForm c = (CenaWinForm)sender;
                    ConcentradorObjeto o = (ConcentradorObjeto)e.Objeto;
                    TreeViewObjetos.Nodes[c.ID].Nodes.Add(o.ID, o.Nome);
                }
            }
            else if (e.TipoAlteracao == TipoAlteracaoLista.Remocao)
            {
                if (e.Objeto is CenaWinForm)
                {
                    CenaWinForm c = (CenaWinForm)e.Objeto;
                    TreeViewObjetos.Nodes.RemoveByKey(c.ID);
                }
                else if (e.Objeto is Script)
                {
                    Script s = (Script)e.Objeto;
                    LstScript.Items.Remove(s);
                    CboUpdate.Items.Remove(s);
                }
                else if (e.Objeto is Som)
                {
                    Som s = (Som)e.Objeto;
                    LstSons.Items.Remove(s);
                }
                else if (e.Objeto is ConcentradorObjeto)
                {
                    ConcentradorObjeto o = (ConcentradorObjeto)e.Objeto;
                    TreeNode node = TreeViewObjetos.Nodes.Find(o.ID, true)[0];
                    node.Remove();
                }
            }
        }
    }
}
