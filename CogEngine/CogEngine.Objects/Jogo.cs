using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using CogEngine.Objects.WinForms;
using CogEngine.Objects.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace CogEngine.Objects
{
    public enum EstruturaProjeto
    {
        PastaSom,
        PastaImagem
    }

    public enum ArquivoPadrao
    {
        ImagemQuadrado,
        ImagemTriangulo,
        ImagemControleJogo,
        DLLObjetos
    }

    public class Jogo
    {
        internal List<ConcentradorObjeto> ListaObjeto { get; private set; }
        internal List<Cena> ListaCena;
        internal List<Script> ListaScripts;
        internal List<Som> ListaSom;
        internal string PastaJogo
        {
            get
            {
                return Arquivo.Remove(Arquivo.LastIndexOf('\\') + 1);
            }
        }

        public event AlteracaoListaEventHandler AlteracaoLista;

        internal string Arquivo { get; private set; }
        private const string PASTA_IMAGEM = "Imagens";
        private const string PASTA_SOM = "Sons";
        public const string EXTENSAO_PROJETO = ".cogproj";
        public const string EXTENSAO_COMPILADO = ".cogcomp";

        private Jogo(string caminhoArquivo)
        {
            Arquivo = caminhoArquivo;
            ListaCena = new List<Cena>();
            ListaScripts = new List<Script>();
            ListaSom = new List<Som>();
            ListaObjeto = new List<ConcentradorObjeto>();
        }

        private void LerArquivoProjeto()
        {
            Type type = typeof(ConcentradorObjeto);
            Type baseInterface;
            ConcentradorObjeto o;
            XmlAttribute attribute;
            PropertyInfo p;

            XmlDocument document = new XmlDocument();
            document.Load(Arquivo);

            XmlNode scripts = document.GetElementsByTagName("Scripts")[0];
            if (scripts.ChildNodes != null)
            {
                foreach (XmlNode scriptNode in scripts.ChildNodes)
                {
                    Script script = new Script();
                    script.NomeAmigavel = scriptNode.Attributes.GetNamedItem("Nome").Value;
                    script.ID = scriptNode.Attributes.GetNamedItem("ID").Value;
                    script.CodigoScript = scriptNode.Attributes.GetNamedItem("Codigo").Value;
                    ListaScripts.Add(script);
                }
            }
            XmlNode jogo = document.GetElementsByTagName("Jogo")[0];

            //Itero as cenas do jogo
            Cena cena;
            foreach (XmlNode cenaNode in jogo.SelectNodes("Cena"))
            {
                cena = new CenaWinForm();
                cena.Nome = cenaNode.Attributes["Nome"].Value;
                cena.Cor = System.Drawing.Color.FromArgb(int.Parse(cenaNode.Attributes["Cor"].Value));

                //Adiciono a lista de cenas para controle
                ListaCena.Add(cena);

                //Itero os objetos
                Type typeOriginal;
                foreach (XmlNode objetoNode in cenaNode.SelectNodes("Objetos/Objeto"))
                {
                    typeOriginal = Assembly.GetAssembly(type).GetType(objetoNode.Attributes["type"].Value);
                    o = (ConcentradorObjeto)typeOriginal.GetConstructor(new Type[] { typeof(Jogo) }).Invoke(new object[] { this });
                    baseInterface = o.BaseInterface;
                    foreach (XmlNode nodeProp in objetoNode.ChildNodes[0].ChildNodes)
                    {
                        attribute = nodeProp.Attributes[0];
                        if (attribute.Name == "Nome")
                        {
                            o.Nome = attribute.Value;
                        }
                        else
                        {
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
                    }

                    //Atribuo o script caso exista
                    if (objetoNode.ChildNodes.Count > 1)
                    {
                        o.WinControl.IDScript = objetoNode.ChildNodes[1].Attributes[1].Value;
                    }
                    cena.AdicionarObjeto(o);
                }
            }

            Som som;
            foreach (XmlNode nodeSom in jogo.SelectNodes("Sons/Som"))
            {
                som = new SomWinForm(this);
                attribute = nodeSom.Attributes["CaminhoArquivo"];
                som.CaminhoRelativo = attribute.Value;
                attribute = nodeSom.Attributes["Nome"];
                som.Nome = attribute.Value;
                ListaSom.Add(som);
            }
        }

        public string RetornarPastaProjeto(EstruturaProjeto estrutura)
        {
            string itemProjeto;
            if (estrutura == EstruturaProjeto.PastaImagem)
                itemProjeto = PASTA_IMAGEM;
            else if (estrutura == EstruturaProjeto.PastaSom)
                itemProjeto = PASTA_SOM;
            else
                return null;
            return PastaJogo + itemProjeto + '\\';
        }

        public string RetornarCaminhoAbsoluto(EstruturaProjeto estrutura, string caminhoRelativo)
        {
            string pasta = RetornarPastaProjeto(estrutura);
            return pasta + caminhoRelativo;
        }

        public string RetornarCaminhoRelativo(EstruturaProjeto estruturaProjeto, string caminhoAbsoluto)
        {
            string pasta = RetornarPastaProjeto(estruturaProjeto);
            return caminhoAbsoluto.Replace(pasta, "");
        }

        public string CopiarArquivoPadrao(ArquivoPadrao arquivoPadrao, EstruturaProjeto estrutura, bool relativo)
        {
            string arquivo = RetornarArquivoPadrao(arquivoPadrao);
            string destino = RetornarCaminhoAbsoluto(EstruturaProjeto.PastaImagem, arquivo);
            if (!File.Exists(destino))
            {
                string caminhoRelativo = Configuracao.PastaArquivos + arquivo;
                FileInfo info = new FileInfo(caminhoRelativo);
                File.Copy(info.FullName, destino);
            }
            if (relativo)
                return RetornarCaminhoRelativo(EstruturaProjeto.PastaImagem, destino);
            else
                return destino;
        }

        public static string RetornarArquivoPadrao(ArquivoPadrao arquivoPadrao)
        {
            if (arquivoPadrao == ArquivoPadrao.ImagemQuadrado)
                return "quadrado.jpg";
            else if (arquivoPadrao == ArquivoPadrao.ImagemTriangulo)
                return "triangulo.JPG";
            else if (arquivoPadrao == ArquivoPadrao.ImagemControleJogo)
                return "video-game-controller.jpg";
            else if (arquivoPadrao == ArquivoPadrao.DLLObjetos)
                return "CogEngine.Objects.dll";
            else
                throw new Exception("Tipo de arquivo padrão desconhecido.");
        }

        public string RetornarCaminhoAbsoluto(EstruturaProjeto estruturaProjeto, ArquivoPadrao arquivoPadrao)
        {
            return RetornarCaminhoAbsoluto(estruturaProjeto, RetornarArquivoPadrao(arquivoPadrao));
        }

        public void AdicionarCena(Cena cena)
        {
            ListaCena.Add(cena);
            if (AlteracaoLista != null)
            {
                AlteracaoLista(this, new AlteracaoListaEventArgs(cena, TipoAlteracaoLista.Adicao));
            }
        }

        public void AdicionarScript(Script script)
        {
            ListaScripts.Add(script);
            if (AlteracaoLista != null)
            {
                AlteracaoLista(this, new AlteracaoListaEventArgs(script, TipoAlteracaoLista.Adicao));
            }
        }

        public void AdicionarSom(Som som)
        {
            ListaSom.Add(som);
            if (AlteracaoLista != null)
            {
                AlteracaoLista(this, new AlteracaoListaEventArgs(som, TipoAlteracaoLista.Adicao));
            }
        }

        public void RemoverCena(Cena cena)
        {
            ListaCena.Remove(cena);
            if (AlteracaoLista != null)
            {
                AlteracaoLista(this, new AlteracaoListaEventArgs(cena, TipoAlteracaoLista.Remocao));
            }
        }

        public void RemoverScript(Script s)
        {
            ListaScripts.Remove(s);
            if (AlteracaoLista != null)
            {
                AlteracaoLista(this, new AlteracaoListaEventArgs(s, TipoAlteracaoLista.Remocao));
            }
        }

        public static Jogo AbrirProjeto(string caminhoArquivo)
        {
            Jogo jogo = new Jogo(caminhoArquivo);
            jogo.LerArquivoProjeto();
            return jogo;
        }

        public static Jogo CriarProjeto(string pasta, string nomeJogo)
        {
            if (!pasta.EndsWith("\\")) pasta += '\\';
            string caminhoArquivo = pasta + nomeJogo + EXTENSAO_PROJETO;
            Jogo jogo = new Jogo(caminhoArquivo);
            CriarEstruturaPasta(pasta, true);
            CenaWinForm cena = new CenaWinForm();
            cena.Nome = "Principal";
            jogo.ListaCena.Add(cena);
            return jogo;
        }

        public static string RetornarCaminhoCompilador()
        {
            return @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe";
        }

        public void Salvar()
        {
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

            foreach (CenaWinForm cena in ListaCena)
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

                ICogEngineWinControl winControl;
                foreach (object c in cena.Painel.Controls)
                {
                    if (c is ICogEngineWinControl)
                    {
                        winControl = (ICogEngineWinControl)c;
                        type = ((ICogEngineWinControl)c).Objeto.BaseInterface;
                        node = xml.CreateNode(XmlNodeType.Element, "Objeto", "");
                        attribute = xml.CreateAttribute("type");
                        attribute.Value = ((ICogEngineWinControl)c).Objeto.GetType().FullName;
                        node.Attributes.Append(attribute);

                        XmlNode nodePropriedades = xml.CreateNode(XmlNodeType.Element, "Propriedades", "");
                        node.AppendChild(nodePropriedades);

                        //Se houver script no objeto, armazeno no xml
                        if (winControl.IDScript != null)
                        {
                            Script s = ListaScripts.FirstOrDefault(script => script.ID == winControl.IDScript);
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
                                if (p.PropertyType == typeof(System.Drawing.Color))
                                {
                                    attribute.Value = ((System.Drawing.Color)p.GetValue(c, null)).ToArgb().ToString();
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
                    }
                }
            }

            //Crio nó para armazenamento dos scripts da engine
            node = xml.CreateNode(XmlNodeType.Element, "Scripts", "");

            foreach (var item in ListaScripts)
            {
                nodeScript = xml.CreateNode(XmlNodeType.Element, "Script", "");

                attribute = xml.CreateAttribute("ID");
                attribute.Value = ((Script)(item)).ID;
                nodeScript.Attributes.Append(attribute);

                attribute = xml.CreateAttribute("Nome");
                attribute.Value = ((Script)(item)).NomeAmigavel;
                nodeScript.Attributes.Append(attribute);

                attribute = xml.CreateAttribute("Codigo");
                attribute.Value = ((Script)(item)).CodigoScript;
                nodeScript.Attributes.Append(attribute);

                node.AppendChild(nodeScript);
            }

            pjt.AppendChild(node);

            XmlNode sons = jogo.AppendChild(xml.CreateNode(XmlNodeType.Element, "Sons", null));

            XmlNode nodeSom;
            foreach (Som som in ListaSom)
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

            //Se o arquivo já existir sobreescrevo
            if (File.Exists(Arquivo)) File.Delete(Arquivo);

            xml.Save(Arquivo);
        }

        public void Compilar(string pasta)
        {
            new CompiladorJogo(this, pasta).Compilar();
        }

        internal static void CriarEstruturaPasta(string pasta, bool criarRoot)
        {
            if (criarRoot)
            {
                Directory.CreateDirectory(pasta);
            }
            Directory.CreateDirectory(pasta + PASTA_IMAGEM);
            Directory.CreateDirectory(pasta + PASTA_SOM);
        }

        public Cena[] ListarCena()
        {
            return ListaCena.ToArray();
        }

        public Script[] ListarScript()
        {
            return ListaScripts.ToArray();
        }

        public Som[] ListarSom()
        {
            return ListaSom.ToArray();
        }

        public static Jogo AbrirJogo(string arquivo, GameProxy gameProxy, ContentManager content, GraphicsDeviceManager graphics)
        {
            Jogo jogo = new Jogo(arquivo);
            jogo.LerArquivoJogo(gameProxy, content, graphics);
            return jogo;
        }

        private void LerArquivoJogo(GameProxy gameProxy, ContentManager content, GraphicsDeviceManager graphics)
        {
            XmlDocument document = new XmlDocument();
            document.Load(Arquivo);
            XmlNode nodeJogo = document.GetElementsByTagName("Jogo")[0];
            PropertyInfo p;
            Type type = typeof(ConcentradorObjeto);
            ConcentradorObjeto o;
            Type typeOriginal;
            Type baseInterface;
            XmlAttribute attribute;
            Cena cena;

            foreach (XmlNode cenaNode in nodeJogo.ChildNodes)
            {
                if (cenaNode.Name == "Cena")
                {
                    cena = new Cena();
                    cena.Nome = cenaNode.Attributes["Nome"].Value;
                    cena.Cor = System.Drawing.Color.FromArgb(int.Parse(cenaNode.Attributes["Cor"].Value));
                    ListaCena.Add(cena);

                    foreach (XmlNode objetoNode in cenaNode.SelectNodes("Objetos/Objeto"))
                    {
                        typeOriginal = Assembly.GetAssembly(type).GetType(objetoNode.Attributes["type"].Value);
                        o = (ConcentradorObjeto)typeOriginal.GetConstructor(new Type[] { typeof(Jogo) }).Invoke(new object[] { this });
                        baseInterface = o.BaseInterface;
                        foreach (XmlNode nodeProp in objetoNode.ChildNodes[0].ChildNodes)
                        {
                            attribute = nodeProp.Attributes[0];
                            if (attribute.Name == "Nome")
                            {
                                o.Nome = attribute.Value;
                            }
                            else
                            {
                                p = baseInterface.GetProperty(attribute.Name);
                                if (p.PropertyType == typeof(int))
                                {
                                    p.SetValue(o.XNAControl, Convert.ToInt32(attribute.Value), null);
                                }
                                else if (p.PropertyType == typeof(float))
                                {
                                    p.SetValue(o.XNAControl, float.Parse(attribute.Value), null);
                                }
                                else if (p.PropertyType == typeof(System.Drawing.Color))
                                {
                                    System.Drawing.Color c = System.Drawing.Color.FromArgb(int.Parse(attribute.Value));
                                    p.SetValue(o.XNAControl, c, null);
                                }
                                else
                                {
                                    p.SetValue(o.XNAControl, attribute.Value, null);
                                }
                            }
                        }
                        if (objetoNode.ChildNodes.Count > 1)
                        {
                            XmlNode nodeScript = objetoNode.ChildNodes[1];
                            string assemblyFile = nodeScript.Attributes["Assembly"].Value;
                            Assembly assembly = Assembly.LoadFrom(assemblyFile);
                            Type userType = assembly.GetTypes()[0];
                            IObjetoScript script = (IObjetoScript)userType.GetConstructor(new Type[]{
                         typeof(GameProxy), typeof(ICogEngineXNAControl)   
                        }).Invoke(new object[] { gameProxy, o.XNAControl });
                            o.Script = script;
                        }
                        o.XNAControl.LoadContent(content, graphics.GraphicsDevice);
                        cena.AdicionarObjeto(o);
                    }
                }
            }

            SomXNA som;
            foreach (XmlNode nodeSom in nodeJogo.SelectNodes("Sons/Som"))
            {
                som = new SomXNA(this);
                attribute = nodeSom.Attributes["CaminhoArquivo"];
                som.CaminhoRelativo = attribute.Value;
                attribute = nodeSom.Attributes["Nome"];
                som.Nome = attribute.Value;
                som.Iniciar();
                ListaSom.Add(som);
            }
        }
    }
}
