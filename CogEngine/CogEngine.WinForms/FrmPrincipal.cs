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

namespace CogEngine.WinForms
{
    public partial class FrmPrincipal : Form
    {
        List<CenaWinForm> _ListaCena;
        CenaWinForm _CenaAtual;

        const string VAZIO = "(Vazio)";

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
            Process.Start(Configuracao.RetornarCaminhoCompilador(), argumentos);
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

        private void BtnAdicionar_Click(object sender, EventArgs e)
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
            XmlNodeList cenas = null;

            string caminhoArquivo = string.Empty;

            op = new OpenFileDialog();
            op.Filter = "XML|*.xml";
            op.Title = "CogEngine - Abrir";
            op.ShowDialog();

            //Obtenho o caminho do txt
            caminhoArquivo = op.FileName;

            if (!string.IsNullOrEmpty(caminhoArquivo) && !string.IsNullOrEmpty(caminhoArquivo))
            {
                ClearEngine();

                //Inicio a Engine
                _ListaCena = new List<CenaWinForm>();
                Configuracao.Iniciar(Plataforma.Forms);
                LoadItems();
                CboUpdate.Items.Add(VAZIO);

                //Carrego o xml e aplico os nós para os controles
                document = new XmlDocument();
                document.Load(caminhoArquivo);
                cenas = document.GetElementsByTagName("Cena");

                //Itero as cenas do jogo
                foreach (XmlNode cena in cenas)
                {
                    //Crio o objeto cena para adiciona-lo ao tree-view
                    CenaWinForm cenaDoc = new CenaWinForm();
                    cenaDoc.Nome = cena.Attributes["Nome"].Value.ToString();
                    cenaDoc.Cor = System.Drawing.Color.FromArgb(int.Parse(cena.Attributes["Cor"].Value.ToString()));
                    cenaDoc.Painel.Size = GrpGameView.Size;
                    cenaDoc.OnNomeChanged += OnNomeChanged;

                    //Adiciono o objeto o tree view
                    TreeViewObjetos.Nodes.Add(cenaDoc.ID, cenaDoc.Nome);

                    //Adiciono a lista de cenas para controle
                    _ListaCena.Add(cenaDoc);


                    //Itero os nós da cena
                    foreach (var objeto in ((XmlNode)cena).ChildNodes)
                    {
                        for (int i = 0; i < ((XmlNode)objeto).ChildNodes.Count; i++)
                        {

                            switch (((XmlNode)objeto).ChildNodes[i].Attributes["type"].Value.ToString())
                            {
                                case "CogEngine.Objects.ConcentradorTexto":

                                    ObjetoAttribute otxt = (ObjetoAttribute)LstControles.Items[1];
                                    ConcentradorObjeto obj = (ConcentradorObjeto)otxt.TipoRelacionado.GetConstructor(Type.EmptyTypes).Invoke(null);

                                    ((CogEngine.Objects.WinForms.TextoWinControl)(obj.WinControl)).PosicaoX = int.Parse(((XmlNode)objeto).ChildNodes[i].ChildNodes[0].ChildNodes[0].Attributes["PosicaoX"].Value.ToString());
                                    ((CogEngine.Objects.WinForms.TextoWinControl)(obj.WinControl)).PosicaoY = int.Parse(((XmlNode)objeto).ChildNodes[i].ChildNodes[0].ChildNodes[1].Attributes["PosicaoY"].Value.ToString());
                                    ((CogEngine.Objects.WinForms.TextoWinControl)(obj.WinControl)).Texto = ((XmlNode)objeto).ChildNodes[i].ChildNodes[0].ChildNodes[2].Attributes["Texto"].Value.ToString();
                                    ((CogEngine.Objects.WinForms.TextoWinControl)(obj.WinControl)).TamanhoFonte = float.Parse(((XmlNode)objeto).ChildNodes[i].ChildNodes[0].ChildNodes[3].Attributes["TamanhoFonte"].Value.ToString());
                                    ((CogEngine.Objects.WinForms.TextoWinControl)(obj.WinControl)).Cor = System.Drawing.Color.FromArgb(int.Parse(((XmlNode)objeto).ChildNodes[i].ChildNodes[0].ChildNodes[4].Attributes["Cor"].Value.ToString()));

                                    if (obj.WinControl != null)
                                    {
                                        Control c = obj.WinControl.InitWinControl();
                                        c.Click += ControClick;
                                        cenaDoc.AdicionarObjeto(obj);
                                        TreeViewObjetos.Nodes[cenaDoc.ID].Nodes.Add(obj.Nome);
                                    }


                                    break;
                                case "CogEngine.Objects.Triangulo":
                                    ObjetoAttribute ot = (ObjetoAttribute)LstControles.Items[0];
                                    ConcentradorObjeto objTriangulo = (ConcentradorObjeto)ot.TipoRelacionado.GetConstructor(Type.EmptyTypes).Invoke(null);

                                    ((CogEngine.Objects.WinForms.FiguraWinControl)(objTriangulo.WinControl)).Altura = int.Parse(((XmlNode)objeto).ChildNodes[i].ChildNodes[0].ChildNodes[0].Attributes["Altura"].Value.ToString());
                                    ((CogEngine.Objects.WinForms.FiguraWinControl)(objTriangulo.WinControl)).Largura = int.Parse(((XmlNode)objeto).ChildNodes[i].ChildNodes[0].ChildNodes[1].Attributes["Largura"].Value.ToString());
                                    ((CogEngine.Objects.WinForms.FiguraWinControl)(objTriangulo.WinControl)).PosicaoX = int.Parse(((XmlNode)objeto).ChildNodes[i].ChildNodes[0].ChildNodes[2].Attributes["PosicaoX"].Value.ToString());
                                    ((CogEngine.Objects.WinForms.FiguraWinControl)(objTriangulo.WinControl)).PosicaoY = int.Parse(((XmlNode)objeto).ChildNodes[i].ChildNodes[0].ChildNodes[3].Attributes["PosicaoY"].Value.ToString());

                                    if (objTriangulo.WinControl != null)
                                    {
                                        Control c = objTriangulo.WinControl.InitWinControl();
                                        c.Click += ControClick;
                                        cenaDoc.AdicionarObjeto(objTriangulo);
                                        TreeViewObjetos.Nodes[cenaDoc.ID].Nodes.Add(objTriangulo.Nome);
                                    }

                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
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
                //sf.CheckFileExists = true;
                sf.CheckPathExists = true;
                sf.FilterIndex = 2;
                sf.RestoreDirectory = true;

                if (sf.ShowDialog() == DialogResult.OK)
                    arquivo = sf.FileName;
                else
                    return;

                //TODO: Verificar arquivo antes de sobreescrever
                if (File.Exists(arquivo))
                    File.Delete(arquivo);

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
            CboUpdate.Items.Add(VAZIO);
        }

        private void ClearEngine()
        {
            LstControles.Items.Clear();
            LstScript.Items.Clear();
            TreeViewObjetos.Nodes.Clear();
            GrpGameView.Controls.Clear();
            ConcentradorTexto._Num = 1;
            Triangulo.Num = 1;
        }
    }
}
