using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CogEngine.Objects
{
    public interface IFigura
    {
        int Altura
        {
            get;
            set;
        }

        int Largura
        {
            get;
            set;
        }

        float PosicaoX
        {
            get;
            set;
        }

        float PosicaoY
        {
            get;
            set;
        }

        int ZIndex
        {
            get;
            set;
        }
    }
}
