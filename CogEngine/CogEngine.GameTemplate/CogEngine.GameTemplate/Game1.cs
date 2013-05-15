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
using System.IO;

namespace CogEngine.GameTemplate
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game, ICogEngineGame
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Jogo _Jogo;
        private Cena _CenaAtual;
        private GameProxy _GameProxy;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            string arquivo = RetornarArquivoJogo();
            _Jogo = Jogo.AbrirJogo(arquivo, _GameProxy, Content, graphics);
            CarregarCena("Principal");
        }

        private string RetornarArquivoJogo()
        {
            string extensao = Jogo.EXTENSAO_COMPILADO;
            string[] arquivos = Directory.GetFiles(Directory.GetCurrentDirectory(), "*" + extensao);
            if (arquivos.Length == 1)
            {
                return arquivos[0];
            }
            else if (arquivos.Length == 0)
            {
                throw new Exception("Não existe arquivo de configuração do jogo");
            }
            else
            {
                throw new Exception("Foram encontrados mais de um arquivo para configuração do jogo");
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
            _CenaAtual = _Jogo.ListarCena().First(c => c.Nome == nomeCena);
        }

        public void Tocar(string nome)
        {
            SomXNA som = (SomXNA)_Jogo.ListarSom().FirstOrDefault(s => s.Nome == nome);
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
