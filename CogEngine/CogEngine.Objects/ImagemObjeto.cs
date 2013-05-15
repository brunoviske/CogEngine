using System;

namespace CogEngine.Objects
{
    [Objeto("Imagem", typeof(ImagemObjeto))]
    public class ImagemObjeto : AbstractImagemObjeto
    {
        public ImagemObjeto(Jogo jogo) : base(jogo) { }

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
                    _CustomXNAImage = new XNA.CustomImageXNAControl(this);
                }
                return _CustomXNAImage;
            }
        }

        public override Type BaseInterface
        {
            get { return typeof(ICustomImage); }
        }

        protected override string Prefixo
        {
            get { return "Imagem"; }
        }
    }
}
