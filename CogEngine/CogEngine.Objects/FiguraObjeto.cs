using System;

namespace CogEngine.Objects
{
    public abstract class FiguraObjeto : AbstractImagemObjeto
    {
        public FiguraObjeto(Jogo jogo) : base(jogo) { }

        /// <summary>
        /// Arquivo padrao do projeto com o nome da imagem usada para contruir a figura
        /// </summary>
        internal abstract ArquivoPadrao Arquivo { get; }
        public override sealed Type BaseInterface
        {
            get { return typeof(IFigura); }
        }
    }
}
