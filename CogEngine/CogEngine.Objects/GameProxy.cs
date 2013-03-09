using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CogEngine.Objects
{
    public class GameProxy : ICogEngineGame
    {
        private ICogEngineGame _Game;

        public GameProxy(ICogEngineGame game)
        {
            _Game = game;
        }

        public void CarregarCena(string nomeCena)
        {
            _Game.CarregarCena(nomeCena);
        }

        public void Tocar(string nome)
        {
            _Game.Tocar(nome);
        }
    }
}
