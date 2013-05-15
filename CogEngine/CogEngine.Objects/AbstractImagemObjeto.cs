using CogEngine.Objects.WinForms;
using CogEngine.Objects.XNA;

namespace CogEngine.Objects
{
    public abstract class AbstractImagemObjeto : ConcentradorObjeto
    {
        public AbstractImagemObjeto(Jogo jogo) : base(jogo) { }

        protected abstract AbstractImageControl FiguraWinControl { get; }
        protected abstract AbstractImagemXNAControl FiguraXNAControl { get; }

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
