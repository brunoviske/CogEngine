using System;

namespace CogEngine.Objects
{
    [Objeto("Imagem", typeof(ImagemObjeto))]
    public class ImagemObjeto : AbstractImagemObjeto
    {
        private string _Nome;
        private WinForms.CustomImageWincontrol _CustomWinImage;
        private XNA.CustomImageXNAControl _CustomXNAImage;

        protected override WinForms.AbstractImageControl FiguraWinControl
        {
            get
            {
                if (_CustomWinImage == null)
                {
                    _CustomWinImage = new WinForms.CustomImageWincontrol(this);
                }
                return _CustomWinImage;
            }
        }

        protected override XNA.AbstractImagemXNAControl FiguraXNAControl
        {
            get
            {
                if (_CustomXNAImage == null)
                {
                    _CustomXNAImage = new XNA.CustomImageXNAControl();
                }
                return _CustomXNAImage;
            }
        }

        public override string Nome
        {
            get
            {
                if (_Nome == null)
                {
                    _Nome = GetNome();
                }
                return _Nome;
            }
            set
            {
                _Nome = value;
            }
        }

        private static int _I = 1;
        private static string GetNome()
        {
            return "Imagem " + _I++;
        }

        public override Type BaseInterface
        {
            get { return typeof(ICustomImage); }
        }
    }
}
