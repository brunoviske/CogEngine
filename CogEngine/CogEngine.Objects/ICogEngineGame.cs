using System.Collections.Generic;
using CogEngine.Objects.XNA;

namespace CogEngine.Objects
{
    public interface ICogEngineGame
    {
        void CarregarCena(string nomeCena);
        void Tocar(string nome);
    }
}
