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
            _ListaCena = new List<CenaWinForm>();
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            Configuracao.Iniciar(Plataforma.Forms);
            LoadItems();
            NewProject();
            CboUpdate.Items.Add(VAZIO);
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
            TreeViewObjetos.Nodes[e.ID].Text = e.NomeNovo;
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
                MessageBox.Show("O tipo do objeto para a operação não era o esperado");
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
            MessageBox.Show("Seu projeto foi compilado com sucesso!");
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
                    MessageBox.Show("O componente para renderização não foi inicializado.");
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
    }
}
