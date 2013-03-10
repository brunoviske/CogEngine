
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

            public class Script_2 : IObjetoScript
            {
                public GameProxy Jogo { get; private set; }
                public ICogEngineXNAControl Objeto { get; private set; }

                public Script_2(GameProxy jogo, ICogEngineXNAControl objeto)
                {
                    Jogo = jogo;
                    Objeto = objeto;
                }

                public void Update(GameTime gameTime)
                {
                    if(Keyboard.GetState().IsKeyDown(Keys.Space))
{
	Jogo.Tocar("hit.wav");
}
                }
            }