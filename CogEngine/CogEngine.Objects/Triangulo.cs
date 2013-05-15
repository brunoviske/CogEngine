using System.Xml.Serialization;
using CogEngine.Objects.WinForms;
using CogEngine.Objects.XNA;

namespace CogEngine.Objects
{
    [Objeto("Triângulo", typeof(Triangulo))]
    public class Triangulo : FiguraObjeto
    {
        public Triangulo(Jogo jogo) : base(jogo) { }

        private FiguraWinControl _TrianguloWinControl;
        private FiguraXNAControl _TrianguloXNAControl;

        protected override AbstractImageControl FiguraWinControl
        {
            get
            {
                if (_TrianguloWinControl == null)
                {
                    _TrianguloWinControl = new FiguraWinControl(this);
                }
                return _TrianguloWinControl;
            }
        }

        protected override AbstractImagemXNAControl FiguraXNAControl
        {
            get
            {
                if (_TrianguloXNAControl == null)
                {
                    _TrianguloXNAControl = new FiguraXNAControl(this);
                }
                return _TrianguloXNAControl;
            }
        }

        internal override ArquivoPadrao Arquivo
        {
            get { return ArquivoPadrao.ImagemTriangulo; }
        }

        protected override string Prefixo
        {
            get { return "Triângulo"; }
        }
    }
}
