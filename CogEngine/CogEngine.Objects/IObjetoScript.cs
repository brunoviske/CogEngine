using CogEngine.Objects.XNA;
using Microsoft.Xna.Framework;

namespace CogEngine.Objects
{
    public interface IObjetoScript
    {
        GameProxy Jogo { get; }
        ICogEngineXNAControl Objeto { get; }
        void Update(GameTime gameTime);
    }
}
