using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using CogEngine.Objects;
using System.Xml;
using System.Reflection;
using CogEngine.Objects.XNA;

namespace CogEngine.GameTemplate
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game, ICogEngineGame
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        

        private List<Cena> _ListaCena;
        private List<SomXNA> _ListaSom;
        private Cena _CenaAtual;
        private GameProxy _GameProxy;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _ListaCena = new List<Cena>();
            _ListaSom = new List<SomXNA>();
            _GameProxy = new GameProxy(this);
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            Configuracao.Iniciar(Plataforma.XNA);

            string arquivo = Configuracao.RetornarArquivoJogo();
            XmlDocument document = new XmlDocument();
            document.Load(arquivo);
            XmlNode jogo = document.GetElementsByTagName("Jogo")[0];
            PropertyInfo p;
            Type type = typeof(ConcentradorObjeto);
            ConcentradorObjeto o;
            Type typeOriginal;
            Type baseInterface;
            XmlAttribute attribute;
            Cena cena;

            foreach (XmlNode cenaNode in jogo.ChildNodes)
            {
                if (cenaNode.Name == "Cena")
                {
                    cena = new Cena();
                    cena.Nome = cenaNode.Attributes["Nome"].Value;
                    cena.Cor = System.Drawing.Color.FromArgb(int.Parse(cenaNode.Attributes["Cor"].Value));
                    _ListaCena.Add(cena);

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
                        if (objetoNode.ChildNodes.Count > 1)
                        {
                            XmlNode nodeScript = objetoNode.ChildNodes[1];
                            string assemblyFile = nodeScript.Attributes["Assembly"].Value;
                            Assembly assembly = Assembly.LoadFrom(assemblyFile);
                            Type userType = assembly.GetTypes()[0];
                            IObjetoScript script = (IObjetoScript)userType.GetConstructor(new Type[]{
                         typeof(GameProxy), typeof(ICogEngineXNAControl)   
                        }).Invoke(new object[] { _GameProxy, o.XNAControl });
                            o.Script = script;
                        }
                        o.XNAControl.LoadContent(Content, graphics.GraphicsDevice);
                        cena.AdicionarObjeto(o);
                    }
                }
                CarregarCena("Principal");
            }

            SomXNA som;
            foreach (XmlNode nodeSom in jogo.SelectNodes("Sons/Som"))
            {
                som = new SomXNA();
                attribute = nodeSom.Attributes["CaminhoArquivo"];
                som.CaminhoCompleto = attribute.Value;
                attribute = nodeSom.Attributes["Nome"];
                som.Nome = attribute.Value;
                som.Iniciar();
                _ListaSom.Add(som);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            foreach (ConcentradorObjeto objeto in _CenaAtual.ListarObjetos())
            {
                if (objeto.Script != null)
                {
                    objeto.Script.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(_CenaAtual.Cor.R, _CenaAtual.Cor.G, _CenaAtual.Cor.B));
            spriteBatch.Begin();

            

            foreach (ConcentradorObjeto item in _CenaAtual.ListarObjetos())
            {
                item.XNAControl.Draw(spriteBatch);
            }

            base.Draw(gameTime);
            spriteBatch.End();
        }

        public void CarregarCena(string nomeCena)
        {
            _CenaAtual = _ListaCena.First(c => c.Nome == nomeCena);
        }

        public void Tocar(string nome)
        {
            SomXNA som = _ListaSom.FirstOrDefault(s => s.Nome == nome);
            if (som != null)
            {
                som.Tocar();
            }
        }

        public object Objeto(string nome)
        {
            ConcentradorObjeto concentrador = _CenaAtual.ListarObjetos().FirstOrDefault(c => c.Nome == nome);
            if (concentrador != null)
            {
                return concentrador.XNAControl;
            }
            else
            {
                return null;
            }
        }

        public void OrdenarZIndex()
        {
            _CenaAtual.Ordenar();
        }
    }
}
