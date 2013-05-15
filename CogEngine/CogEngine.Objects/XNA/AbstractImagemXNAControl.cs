using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;
using Microsoft.Xna.Framework.Content;

namespace CogEngine.Objects.XNA
{
    public abstract class AbstractImagemXNAControl : ICogEngineXNAControl
    {
        protected AbstractImagemObjeto Objeto { get; private set; }

        public AbstractImagemXNAControl(AbstractImagemObjeto objeto)
        {
            Objeto = objeto;
        }

        public Texture2D Textura
        {
            get;
            private set;
        }

        public abstract string CaminhoImagem { get; set; }

        private string CaminhoAbsolutoImagem
        {
            get
            {
                return Objeto.Jogo.RetornarCaminhoAbsoluto(EstruturaProjeto.PastaImagem, CaminhoImagem);
            }
        }

        public void LoadContent(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            using (FileStream f = new FileStream(CaminhoAbsolutoImagem, FileMode.Open))
            {
                Textura = Texture2D.FromStream(graphicsDevice, f);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Textura == null)
            {
                throw new Exception("O objeto Texture2D não foi inicializado. Faça chamada ao método LoadTexture antes.");
            }
            spriteBatch.Draw(Textura, new Rectangle(Convert.ToInt32(PosicaoX), Convert.ToInt32(PosicaoY), Largura, Altura), Color.White);
        }

        public int Altura
        {
            get;
            set;
        }

        public int Largura
        {
            get;
            set;
        }

        public float PosicaoX
        {
            get;
            set;
        }

        public float PosicaoY
        {
            get;
            set;
        }

        public int ZIndex
        {
            get;
            set;
        }

        public new Type GetType()
        {
            return typeof(AbstractImagemXNAControl);
        }
    }
}