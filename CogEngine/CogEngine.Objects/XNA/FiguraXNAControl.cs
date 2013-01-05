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
    public class FiguraXNAControl : ICogEngineXNAControl, IFigura
    {
        private FiguraObjeto _Figura;

        public FiguraXNAControl(FiguraObjeto figura)
        {
            _Figura = figura;
        }

        public Texture2D Textura
        {
            get;
            private set;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Textura == null)
            {
                throw new Exception("O objeto Texture2D não foi inicializado. Faça chamada ao método LoadTexture antes.");
            }
            spriteBatch.Draw(Textura, new Vector2(PosicaoX, PosicaoY), Color.White);
        }

        public void LoadContent(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            using (FileStream f = new FileStream(_Figura.CaminhoArquivo, FileMode.Open))
            {
                Textura = Texture2D.FromStream(graphicsDevice, f, Largura, Altura, false);
            }
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

        public void Update(GameTime gameTime)
        {
            
        }
    }
}
