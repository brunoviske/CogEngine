using System;

namespace CogEngine.Objects
{
    public abstract class FiguraObjeto : AbstractImagemObjeto
    {
        internal abstract string CaminhoArquivo { get; }
        public override sealed Type BaseInterface
        {
            get { return typeof(IFigura); }
        }
    }
}
