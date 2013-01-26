using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CogEngine.Objects.XNA
{
    public interface ICogEngineXNAControl
    {
        void Draw(SpriteBatch spriteBatch);
        void LoadContent(ContentManager contentManager, GraphicsDevice graphicsDevice);
        int ZIndex { get; set; }
    }
}
