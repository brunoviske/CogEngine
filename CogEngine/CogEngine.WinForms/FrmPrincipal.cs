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
        List<CenaWinForm> _ListaCena;
        CenaWinForm _CenaAtual;
        ToolTip toolMaxRest = new ToolTip();
        
        const string VAZIO = "(Vazio)";

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
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            LoadEngine();
        }

        private void NewProject()
        {
            AdicionarCena("Principal");
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
            TreeViewObjetos.Nodes.Add(cena.ID, nome);
            _ListaCena.Add(cena);
            CarregarCena(cena);
            return cena;
        }

        private void OnNomeChanged(object sender, NomeChangedEvent e)
        {
            try
            {
                TreeViewObjetos.Nodes[e.ID].Text = e.NomeNovo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Falha ao atribuir o nome ao componente. Detalhes: {0}", ex.Message), "CogEngine - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                string pasta = Configuracao.RetornarPastaTemp(); ;
                ExcluirArquivo(pasta);
                string arquivo = Configuracao.RetornarArquivoJogo();
                XmlDocument xml = new XmlDocument();
                XmlNode node = xml.CreateNode(XmlNodeType.XmlDeclaration, "xml", null);
                xml.AppendChild(node);

                XmlElement jogo = xml.CreateElement("Jogo");
                xml.AppendChild(jogo);

                XmlElement controles;
                XmlAttribute attribute;
                XmlNode nodeCena;
                XmlNode nodeProp;
                Type type;

                foreach (CenaWinForm cena in _ListaCena)
                {
                    nodeCena = xml.CreateNode(XmlNodeType.Element, "Cena", "");
                    jogo.AppendChild(nodeCena);

                    attribute = xml.CreateAttribute("Nome");
                    attribute.Value = cena.Nome;
                    nodeCena.Attributes.Append(attribute);

                    attribute = xml.CreateAttribute("Cor");
                    attribute.Value = cena.Cor.ToArgb().ToString();
                    nodeCena.Attributes.Append(attribute);
                    controles = xml.CreateElement("Objetos");
                    nodeCena.AppendChild(controles);

                    foreach (Control c in cena.Painel.Controls)
                    {
                        if (c is ICogEngineWinControl)
                        {
                            type = ((ICogEngineWinControl)c).Objeto.BaseInterface;
                            node = xml.CreateNode(XmlNodeType.Element, "Objeto", "");
                            attribute = xml.CreateAttribute("type");
                            attribute.Value = ((ICogEngineWinControl)c).Objeto.GetType().FullName;
                            node.Attributes.Append(attribute);

                            XmlNode nodePropriedades = xml.CreateNode(XmlNodeType.Element, "Propriedades", "");
                            node.AppendChild(nodePropriedades);

                            foreach (PropertyInfo p in type.GetProperties())
                            {
                                if (p.CanWrite)
                                {
                                    nodeProp = xml.CreateNode(XmlNodeType.Element, "Propriedade", "");
                                    attribute = xml.CreateAttribute(p.Name);
                                    if (p.PropertyType == typeof(Color))
                                    {
                                        attribute.Value = ((Color)p.GetValue(c, null)).ToArgb().ToString();
                                    }
                                    else
                                    {
                                        attribute.Value = Convert.ToString(p.GetValue(c, null));
                                    }
                                    nodeProp.Attributes.Append(attribute);
                                    nodePropriedades.AppendChild(nodeProp);
                                }
                            }
                            AtrelarScript((ICogEngineWinControl)c, node);
                            controles.AppendChild(node);
                        }
                    }
                }
                xml.Save(arquivo);
                MessageBox.Show("Seu projeto foi compilado com sucesso!", "CogEngine - compilação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao compilar projeto: " + ex.Message, "Erro - compilação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AtrelarScript(ICogEngineWinControl control, XmlNode node)
        {
            if (control.IDScript != null)
            {
                Script s = RetornarScript(control.IDScript);
                if (s != null)
                {
                    string dll = GerarDll(s);
                    XmlNode nodeScript = node.OwnerDocument.CreateNode(XmlNodeType.Element, "Script", "");

                    XmlAttribute attribute = node.OwnerDocument.CreateAttribute("Assembly");
                    attribute.Value = dll;
                    nodeScript.Attributes.Append(attribute);

                    attribute = node.OwnerDocument.CreateAttribute("Namespace");
                    nodeScript.Attributes.Append(attribute);

                    node.AppendChild(nodeScript);
                }
            }
        }

        private string GerarDll(Script script)
        {
            string filePath = EscreverClasse(script);
            string dll = Configuracao.RetornarPastaTemp() + "\\" + script.NomeClasse + ".dll";
            string argumentos = @"/target:library /out:""{0}"" ""{1}"" /reference:""{2}"" /reference:""{3}\Microsoft.Xna.Framework.dll"" /reference:""{3}\Microsoft.Xna.Framework.Game.dll""";
            argumentos = string.Format(argumentos, dll, filePath, Configuracao.RetornarReferenciaCogEngine(), Configuracao.RetornarPastaXNA());
            Process p = new Process();
            p.StartInfo.FileName = Configuracao.RetornarCaminhoCompilador();
            p.StartInfo.Arguments = argumentos;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            string erro = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            if (p.ExitCode != 0)
            {
                int i = erro.IndexOf("error ");
                throw new Exception(erro.Substring(i));
            }
            return dll;
        }

        private string EscreverClasse(Script s)
        {
            string classe = @"
            using System;
            using CogEngine.Objects;
            using CogEngine.Objects.XNA;
            using Microsoft.Xna.Framework;
            using Microsoft.Xna.Framework.Audio;
            using Microsoft.Xna.Framework.Content;
            using Microsoft.Xna.Framework.GamerServices;
            using Microsoft.Xna.Framework.Graphics;
            using Microsoft.Xna.Framework.Input;
            using Microsoft.Xna.Framework.Media;

            public class " + s.NomeClasse + @" : IObjetoScript
            {
                public GameProxy Jogo { get; private set; }
                public ICogEngineXNAControl Objeto { get; private set; }

                public " + s.NomeClasse + @"(GameProxy jogo, ICogEngineXNAControl objeto)
                {
                    Jogo = jogo;
                    Objeto = objeto;
                }

                public void Update(GameTime gameTime)
                {
                    " + s.CodigoScript + @"
                }
            }";
            string filePath = Configuracao.RetornarPastaTemp() + "\\" + s.NomeClasse + ".cs";
            File.WriteAllText(filePath, classe);
            return filePath;
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
                ConcentradorObjeto objeto = (ConcentradorObjeto)o.TipoRelacionado.GetConstructor(Type.EmptyTypes).Invoke(null);
                if (objeto.WinControl != null)
                {
                    Control c = objeto.WinControl.InitWinControl();
                    c.Click += ControClick;
                    _CenaAtual.AdicionarObjeto(objeto);
                    TreeViewObjetos.Nodes[_CenaAtual.ID].Nodes.Add(objeto.Nome);
                }
                else
                {
                    MessageBox.Show("O componente para renderização não foi inicializado.", "CogEngine - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                cena = _ListaCena.First(c => c.Nome == e.Node.Parent.Text);
                ConcentradorObjeto objeto = cena.ListarObjetos().First(f => f.Nome == e.Node.Text);
                CarregarDetalhe(objeto.WinControl);
            }
            else
            {
                cena = _ListaCena.First(c => c.Nome == e.Node.Text);
                CarregarCena(cena);
                CarregarDetalhe(cena);
            }
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
                    LstScript.Items.Add(script);
                    CboUpdate.Items.Add(script);
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
            OpenFileDialog op = null;
            XmlDocument document = null;

            
            PropertyInfo p;
            Type type = typeof(ConcentradorObjeto);
            ConcentradorObjeto o;
            Type typeOriginal;
            Type baseInterface;
            XmlAttribute attribute;
            CenaWinForm cena = null;
            string caminhoArquivo = string.Empty;

            op = new OpenFileDialog();
            op.Filter = "XML|*.xml";
            op.Title = "CogEngine - Abrir";
            op.ShowDialog();

            //Obtenho o caminho do XML
            caminhoArquivo = op.FileName;

            if (!string.IsNullOrEmpty(caminhoArquivo) && !string.IsNullOrEmpty(caminhoArquivo))
            {
                ClearEngine();

                //Inicio a Engine
                _ListaCena = new List<CenaWinForm>();
                Configuracao.Iniciar(Plataforma.Forms);
                LoadItems();
                CboUpdate.Items.Add(VAZIO);
                CboUpdate.SelectedIndex = 0;

                document = new XmlDocument();
                document.Load(caminhoArquivo);

                XmlNode scripts = document.GetElementsByTagName("Scripts")[0];

                foreach (XmlNode scriptNode in scripts.ChildNodes)
                {
                    Script script = new Script();
                    script.NomeAmigavel = scriptNode.Attributes.GetNamedItem("Nome").Value;
                    script.ID = scriptNode.Attributes.GetNamedItem("ID").Value;
                    script.CodigoScript = scriptNode.Attributes.GetNamedItem("Codigo").Value;
                    LstScript.Items.Add(script);
                    CboUpdate.Items.Add(script);
                }

                XmlNode jogo = document.GetElementsByTagName("Jogo")[0];

                //Itero as cenas do jogo
                foreach (XmlNode cenaNode in jogo.ChildNodes)
                {
                    cena = new CenaWinForm();
                    cena.Nome = cenaNode.Attributes["Nome"].Value;
                    cena.Cor = System.Drawing.Color.FromArgb(int.Parse(cenaNode.Attributes["Cor"].Value));
                    
                    cena.Painel.Size = GrpGameView.Size;
                    cena.OnNomeChanged += OnNomeChanged;
                    
                    //Adiciono o objeto o tree view
                    TreeViewObjetos.Nodes.Add(cena.ID, cena.Nome);

                    //Adiciono a lista de cenas para controle
                    _ListaCena.Add(cena);

                    //Itero os objetos
                    foreach (XmlNode objetoNode in cenaNode.SelectNodes("Objetos/Objeto"))
                    {
                        typeOriginal = Assembly.GetAssembly(type).GetType(objetoNode.Attributes["type"].Value);
                        o = (ConcentradorObjeto)typeOriginal.GetConstructor(Type.EmptyTypes).Invoke(null);
                        baseInterface = o.BaseInterface;
                        foreach (XmlNode nodeProp in objetoNode.ChildNodes[0].ChildNodes)
                        {
                            attribute = nodeProp.Attributes[0];
                            p = baseInterface.GetProperty(attribute.Name);
                            if (p.PropertyType == typeof(int))
                            {
                                p.SetValue(o.WinControl, Convert.ToInt32(attribute.Value), null);
                            }
                            else if (p.PropertyType == typeof(float))
                            {
                                p.SetValue(o.WinControl, float.Parse(attribute.Value), null);
                            }
                            else if (p.PropertyType == typeof(System.Drawing.Color))
                            {
                                System.Drawing.Color c = System.Drawing.Color.FromArgb(int.Parse(attribute.Value));
                                p.SetValue(o.WinControl, c, null);
                            }
                            else
                            {
                                p.SetValue(o.WinControl, attribute.Value, null);
                            }
                        }

                        //Atribuo o script caso exista
                        if (objetoNode.ChildNodes.Count > 1) 
                            o.WinControl.IDScript = objetoNode.ChildNodes[1].Attributes[1].Value;

                        //Crio o controle, adiciono seu evento para exibição dos detalhes e os adiciono na cena                       
                        Control ctr = o.WinControl.InitWinControl();
                        ctr.Click += ControClick;
                        cena.AdicionarObjeto(o);
                        TreeViewObjetos.Nodes[cena.ID].Nodes.Add(o.Nome);                     
                    }
                }
            }

            //Carrego a primeira cena caso haja alguma
            if (_ListaCena.Count > 0)
                CarregarCena(_ListaCena[0]);
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string arquivo = string.Empty;

                SaveFileDialog sf = new SaveFileDialog();
                sf.InitialDirectory = Configuracao.RetornarPastaTemp();
                sf.Title = "CogEngine - Salvar Projeto";
                sf.DefaultExt = "xml";
                sf.Filter = "XML|*.xml";
                sf.CheckPathExists = true;
                sf.FilterIndex = 2;
                sf.RestoreDirectory = true;

                if (sf.ShowDialog() == DialogResult.OK)
                    arquivo = sf.FileName;
                else
                    return;
                
                XmlDocument xml = new XmlDocument();
                XmlNode node = xml.CreateNode(XmlNodeType.XmlDeclaration, "xml", null);
                xml.AppendChild(node);

                XmlElement pjt = xml.CreateElement("Projeto");
                xml.AppendChild(pjt);

                XmlElement jogo = xml.CreateElement("Jogo");
                pjt.AppendChild(jogo);
                
                XmlElement controles;
                XmlAttribute attribute;
                XmlNode nodeCena;
                XmlNode nodeProp;
                XmlNode nodeScript;
                Type type;

                foreach (CenaWinForm cena in _ListaCena)
                {
                    nodeCena = xml.CreateNode(XmlNodeType.Element, "Cena", "");
                    jogo.AppendChild(nodeCena);

                    attribute = xml.CreateAttribute("Nome");
                    attribute.Value = cena.Nome;
                    nodeCena.Attributes.Append(attribute);

                    attribute = xml.CreateAttribute("Cor");
                    attribute.Value = cena.Cor.ToArgb().ToString();
                    nodeCena.Attributes.Append(attribute);
                    controles = xml.CreateElement("Objetos");
                    nodeCena.AppendChild(controles);

                    foreach (Control c in cena.Painel.Controls)
                    {
                        if (c is ICogEngineWinControl)
                        {
                            type = ((ICogEngineWinControl)c).Objeto.BaseInterface;
                            node = xml.CreateNode(XmlNodeType.Element, "Objeto", "");
                            attribute = xml.CreateAttribute("type");
                            attribute.Value = ((ICogEngineWinControl)c).Objeto.GetType().FullName;
                            node.Attributes.Append(attribute);

                            XmlNode nodePropriedades = xml.CreateNode(XmlNodeType.Element, "Propriedades", "");
                            node.AppendChild(nodePropriedades);

                            //Se houver script no objeto, armazeno no xml
                            if (((ICogEngineWinControl)c).IDScript != null)
                            {
                                Script s = RetornarScript(((ICogEngineWinControl)c).IDScript);
                                if (s != null)
                                {
                                    nodeScript = node.OwnerDocument.CreateNode(XmlNodeType.Element, "ScriptObjeto", "");

                                    attribute = node.OwnerDocument.CreateAttribute("NomeScript");
                                    attribute.Value = s.NomeAmigavel;
                                    nodeScript.Attributes.Append(attribute);

                                    attribute = node.OwnerDocument.CreateAttribute("IDScript");
                                    attribute.Value = s.ID;
                                    nodeScript.Attributes.Append(attribute);

                                    node.AppendChild(nodeScript);
                                }
                            }
                            
                            controles.AppendChild(node);

                            foreach (PropertyInfo p in type.GetProperties())
                            {
                                if (p.CanWrite)
                                {
                                    nodeProp = xml.CreateNode(XmlNodeType.Element, "Propriedade", "");
                                    attribute = xml.CreateAttribute(p.Name);
                                    if (p.PropertyType == typeof(Color))
                                    {
                                        attribute.Value = ((Color)p.GetValue(c, null)).ToArgb().ToString();
                                    }
                                    else
                                    {
                                        attribute.Value = Convert.ToString(p.GetValue(c, null));
                                    }
                                    nodeProp.Attributes.Append(attribute);
                                    nodePropriedades.AppendChild(nodeProp);
                                }
                            }                            
                        }
                    }
                }

                //Crio nó para armazenamento dos scripts da engine
                node = xml.CreateNode(XmlNodeType.Element, "Scripts", "");

                foreach (var item in LstScript.Items)
                {
                    nodeScript = xml.CreateNode(XmlNodeType.Element, "Script", "");

                    attribute = xml.CreateAttribute("ID");
                    attribute.Value = ((CogEngine.WinForms.Script)(item)).ID;
                    nodeScript.Attributes.Append(attribute);

                    attribute = xml.CreateAttribute("Nome");
                    attribute.Value = ((CogEngine.WinForms.Script)(item)).NomeAmigavel;
                    nodeScript.Attributes.Append(attribute);

                    attribute = xml.CreateAttribute("Codigo");
                    attribute.Value = ((CogEngine.WinForms.Script)(item)).CodigoScript;
                    nodeScript.Attributes.Append(attribute);

                    node.AppendChild(nodeScript);
                }

                pjt.AppendChild(node);

                //Se o arquivo já existir sobreescrevo
                if (File.Exists(arquivo))
                    File.Delete(arquivo);
                
                xml.Save(arquivo);
                
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
            ClearEngine();
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
            ClearEngine();
            LoadEngine();
        }
        #endregion

        private void LoadEngine()
        {
            _ListaCena = new List<CenaWinForm>();
            Configuracao.Iniciar(Plataforma.Forms);
            LoadItems();
            NewProject();
            SetToolTip();
            CboUpdate.Items.Add(VAZIO);
            CboUpdate.SelectedIndex = 0;
        }

        private void SetToolTip()
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btnFechar, "Fechar");
            tt.SetToolTip(btnMinimizar, "Minimizar");

            toolMaxRest.ShowAlways = true;
        }

        private void ClearEngine()
        {
            LstControles.Items.Clear();
            LstScript.Items.Clear();
            TreeViewObjetos.Nodes.Clear();
            GrpGameView.Controls.Clear();
            CboUpdate.Items.Clear();
            ConcentradorTexto._Num = 1;
            Triangulo.Num = 1;
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
    }
}
