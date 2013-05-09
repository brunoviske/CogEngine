
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

            public class Script_2 : IObjetoScript
            {
                public GameProxy Jogo { get; private set; }
                public ICogEngineXNAControl Objeto { get; private set; }
                private Dicionario<string, object> Dados;

                public Script_2(GameProxy jogo, ICogEngineXNAControl objeto)
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
                    if(Keyboard.GetState().IsKeyDown(Keys.Space))
{
    object o = Jogo.Objeto("Img");
    AlterarPropriedade(o, "ZIndex", 1);
    Jogo.OrdenarZIndex();
}
                }
            }