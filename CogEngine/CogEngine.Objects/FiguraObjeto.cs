using System;
using CogEngine.Objects.WinForms;
using CogEngine.Objects.XNA;

namespace CogEngine.Objects
{
    public abstract class FiguraObjeto : ConcentradorObjeto
    {
        internal abstract string CaminhoArquivo { get; }
        public override sealed Type BaseInterface
        {
            get { return typeof(IFigura); }
        }

        protected abstract FiguraWinControl FiguraWinControl { get; }
        protected abstract FiguraXNAControl FiguraXNAControl { get; }

        public override sealed ICogEngineWinControl WinControl
        {
            get { return FiguraWinControl; }
        }

        public override sealed ICogEngineXNAControl XNAControl
        {
            get { return FiguraXNAControl; }
        }
    }
}
