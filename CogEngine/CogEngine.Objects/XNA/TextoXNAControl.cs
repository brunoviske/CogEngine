using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CogEngine.Objects.XNA
{
    public class TextoXNAControl : ICogEngineXNAControl, ITexto
    {
        private ConcentradorTexto _ConcentradorTexto;
        private SpriteFont _Font;

        public TextoXNAControl(ConcentradorTexto concentradorTexto)
        {
            _ConcentradorTexto = concentradorTexto;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_Font, Texto
                , new Vector2(PosicaoX, PosicaoY)
                , new Color(Cor.R, Cor.G, Cor.B)
            );
        }

        public string Texto
        {
            get;
            set;
        }

        public float TamanhoFonte
        {
            get;
            set;
        }

        public System.Drawing.Color Cor
        {
            get;
            set;
        }

        public int PosicaoX
        {
            get;
            set;
        }

        public int PosicaoY
        {
            get;
            set;
        }

        public int ZIndex
        {
            get;
            set;
        }

        public void LoadContent(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            _Font = contentManager.Load<SpriteFont>("FontePadrao");
        }


        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}
