﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

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

        public object Objeto(string nome)
        {
            return _Game.Objeto(nome);
        }

        public void OrdenarZIndex()
        {
            _Game.OrdenarZIndex();
        }
    }
}
