using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.IO;

namespace CogEngine.Objects.WinForms
{
    public class CustomImageWincontrol : AbstractImageControl, ICustomImage
    {
        private ConcentradorObjeto _Objeto;

        internal CustomImageWincontrol(ConcentradorObjeto objeto)
        {
            _Objeto = objeto;
            Altura = 80;
        }

        public override System.Windows.Forms.Control InitWinControl()
        {
            CaminhoImagem = Configuracao.RetornarPastaArquivos() + "\\video-game-controller.jpg";
            return base.InitWinControl();
        }

        [Browsable(false)]
        public override ConcentradorObjeto Objeto
        {
            get { return _Objeto; }
        }

        public Image Imagem
        {
            get
            {
                return Image;
            }
            set
            {
                Image = value;
                _CaminhoImagem = new ManipuladorArquivo().SalvarImagem(value);
            }
        }

        private string _CaminhoImagem;

        [Browsable(false)]
        public string CaminhoImagem
        {
            get
            {
                return _CaminhoImagem;
            }
            set
            {
                _CaminhoImagem = value;
                Image = new System.Drawing.Bitmap(value);
            }
        }
    }
}
