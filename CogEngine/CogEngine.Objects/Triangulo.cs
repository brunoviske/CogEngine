using System.Xml.Serialization;
using CogEngine.Objects.WinForms;
using CogEngine.Objects.XNA;

namespace CogEngine.Objects
{
    [Objeto("Triângulo", typeof(Triangulo))]
    public class Triangulo : FiguraObjeto
    {
        private FiguraWinControl _TrianguloWinControl;
        private FiguraXNAControl _TrianguloXNAControl;
        private string _Nome;

        protected override FiguraWinControl FiguraWinControl
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

        protected override FiguraXNAControl FiguraXNAControl
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

        internal override string CaminhoArquivo
        {
            get { return Configuracao.RetornarPastaImagens() + "\\triangulo.JPG"; }
        }

        public override string Nome
        {
            get
            {
                if (_Nome == null)
                {
                    _Nome = Triangulo.GetNome();
                }
                return _Nome;
            }
            set
            {
                _Nome = value;
            }
        }

        private static int Num = 1;
        private static string GetNome()
        {
            return "Triângulo " + Num++;
        }
    }
}
