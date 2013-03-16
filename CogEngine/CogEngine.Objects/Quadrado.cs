using System.Xml.Serialization;
using CogEngine.Objects.WinForms;
using CogEngine.Objects.XNA;

namespace CogEngine.Objects
{
    [Objeto("Quadrado", typeof(Quadrado))]
    public class Quadrado : FiguraObjeto
    {
        private FiguraWinControl _QuadradoWinControl;
        private FiguraXNAControl _QuadradoXNAControl;
        private string _Nome;

        protected override AbstractImageControl FiguraWinControl
        {
            get
            {
                if (_QuadradoWinControl == null)
                {
                    _QuadradoWinControl = new FiguraWinControl(this);
                }
                return _QuadradoWinControl;
            }
        }

        protected override AbstractImagemXNAControl FiguraXNAControl
        {
            get
            {
                if (_QuadradoXNAControl == null)
                {
                    _QuadradoXNAControl = new FiguraXNAControl(this);
                }
                return _QuadradoXNAControl;
            }
        }

        internal override string CaminhoArquivo
        {
            get { return Configuracao.RetornarPastaArquivos() + "\\quadrado.jpg"; }
        }

        public override string Nome
        {
            get
            {
                if (_Nome == null)
                {
                    _Nome = Quadrado.GetNome();
                }
                return _Nome;
            }
            set
            {
                _Nome = value;
            }
        }

        public static int Num = 1;
        private static string GetNome()
        {
            return "Quadrado " + Num++;
        }
    }
}
