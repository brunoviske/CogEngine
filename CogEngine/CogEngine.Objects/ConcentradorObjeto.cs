using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CogEngine.Objects.WinForms;
using CogEngine.Objects.XNA;

namespace CogEngine.Objects
{
    public abstract class ConcentradorObjeto
    {
        public abstract string Nome { get; set; }
        public abstract ICogEngineWinControl WinControl { get; }
        public abstract ICogEngineXNAControl XNAControl { get; }
        public abstract Type BaseInterface { get; }
        public IObjetoScript Script { get; set; }
    }
}
