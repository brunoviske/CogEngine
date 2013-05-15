using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using CogEngine.Objects.WinForms;

namespace CogEngine.Objects
{
    class CompiladorJogo
    {
        private Jogo _Jogo;
        private string _PastaVersaoCompilada;
        private string _NomeProjeto;

        public CompiladorJogo(Jogo jogo, string pastaVersaoCompilada)
        {
            _Jogo = jogo;

            string nomeProjeto = jogo.Arquivo.Substring(jogo.Arquivo.LastIndexOf('\\') + 1);
            _NomeProjeto = nomeProjeto.Remove(nomeProjeto.LastIndexOf('.'));

            _PastaVersaoCompilada = pastaVersaoCompilada.EndsWith("\\") ? pastaVersaoCompilada : pastaVersaoCompilada + '\\';
            if(_PastaVersaoCompilada == _Jogo.PastaJogo) throw new Exception("A pasta selecionada não pode ser a pasta do projeto");
        }

        internal void Compilar()
        {
            _Jogo.Salvar();
            Jogo.CriarEstruturaPasta(_PastaVersaoCompilada, false);
            CopiarTodosArquivos(EstruturaProjeto.PastaImagem);
            CopiarTodosArquivos(EstruturaProjeto.PastaSom);
            CopiarJogoTemplate();
            string arquivo = _PastaVersaoCompilada + _NomeProjeto + Jogo.EXTENSAO_COMPILADO;
            EscreverArquivoCompilado(arquivo);
        }

        private void CopiarJogoTemplate()
        {
            string pasta = Configuracao.PastaJogoTemplate;
            string pastaDestino;
            string arquivoDest;
            foreach (string s in Directory.GetFiles(pasta, "*.*", SearchOption.AllDirectories))
            {
                pastaDestino = _PastaVersaoCompilada + s.Substring(pasta.Length);
                pastaDestino = pastaDestino.Remove(pastaDestino.LastIndexOf('\\'));
                if (!Directory.Exists(pastaDestino)) Directory.CreateDirectory(pastaDestino);
                arquivoDest = pastaDestino + s.Substring(s.LastIndexOf('\\'));
                File.Copy(s, arquivoDest, true);
            }
        }

        private void CopiarTodosArquivos(EstruturaProjeto estruturaProjeto)
        {
            string origem = _Jogo.RetornarPastaProjeto(estruturaProjeto);
            if(origem.EndsWith("\\")) origem = origem.Remove(origem.Length - 1);
            string pastaDestino = _PastaVersaoCompilada + origem.Substring(origem.LastIndexOf('\\') + 1);
            string arquivoDest;
            foreach (string s in Directory.GetFiles(origem))
            {
                arquivoDest = pastaDestino + '\\' + s.Substring(s.LastIndexOf('\\') + 1);
                File.Copy(s, arquivoDest, true);
            }
        }

        private void EscreverArquivoCompilado(string arquivo)
        {
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

            foreach (CenaWinForm cena in _Jogo.ListaCena)
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
                        nodeProp = xml.CreateNode(XmlNodeType.Element, "Propriedade", "");
                        attribute = xml.CreateAttribute("Nome");
                        attribute.Value = ((ICogEngineWinControl)c).Objeto.Nome;
                        nodeProp.Attributes.Append(attribute);
                        nodePropriedades.AppendChild(nodeProp);

                        AtrelarScript((ICogEngineWinControl)c, node);
                        controles.AppendChild(node);
                    }
                }
            }

            XmlNode sons = jogo.AppendChild(xml.CreateNode(XmlNodeType.Element, "Sons", null));

            XmlNode nodeSom;
            foreach (Som som in _Jogo.ListaSom)
            {
                nodeSom = xml.CreateNode(XmlNodeType.Element, "Som", null);
                attribute = xml.CreateAttribute("CaminhoArquivo");
                attribute.Value = som.CaminhoRelativo;
                nodeSom.Attributes.Append(attribute);

                attribute = xml.CreateAttribute("Nome");
                attribute.Value = som.Nome;
                nodeSom.Attributes.Append(attribute);

                sons.AppendChild(nodeSom);
            }

            xml.Save(arquivo);
        }

        private void AtrelarScript(ICogEngineWinControl control, XmlNode node)
        {
            if (control.IDScript != null)
            {
                Script s = _Jogo.ListaScripts.FirstOrDefault(scpt => scpt.ID == control.IDScript);
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
            string dll = _PastaVersaoCompilada + script.NomeClasse + ".dll";
            string argumentos = @"/target:library /out:""{0}"" ""{1}"" /reference:""{2}"" /reference:""{3}\Microsoft.Xna.Framework.dll"" /reference:""{3}\Microsoft.Xna.Framework.Game.dll""";
            argumentos = string.Format(argumentos, dll, filePath, _PastaVersaoCompilada + Jogo.RetornarArquivoPadrao(ArquivoPadrao.DLLObjetos), Configuracao.RetornarPastaXNA());
            Process p = new Process();
            p.StartInfo.FileName = Jogo.RetornarCaminhoCompilador();
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
            using System.Reflection;

            public class " + s.NomeClasse + @" : IObjetoScript
            {
                public GameProxy Jogo { get; private set; }
                public ICogEngineXNAControl Objeto { get; private set; }
                private Dicionario<string, object> Dados;

                public " + s.NomeClasse + @"(GameProxy jogo, ICogEngineXNAControl objeto)
                {
                    Jogo = jogo;
                    Objeto = objeto;
                    Dados = new Dicionario<string, object>();
                }

                private void AlterarPropriedade(object objeto, string propriedade, object valor)
                {
                    Type tipo = objeto.GetType();
                    PropertyInfo p = tipo.GetProperty(propriedade);
                    if(p != null)
                    {
                        p.SetValue(objeto, valor, null);
                    }
                }

                private object Valor(object objeto, string propriedade)
                {
                    Type tipo = objeto.GetType();
                    PropertyInfo p = tipo.GetProperty(propriedade);
                    if(p != null)
                    {
                        return p.GetValue(objeto, null);
                    }
                    else
                    {
                        return null;
                    }
                }

                public void Update(GameTime gameTime)
                {
                    " + s.CodigoScript + @"
                }
            }";
            string filePath = Configuracao.PastaTemp + s.NomeClasse + ".cs";
            File.WriteAllText(filePath, classe);
            return filePath;
        }
    }
}
