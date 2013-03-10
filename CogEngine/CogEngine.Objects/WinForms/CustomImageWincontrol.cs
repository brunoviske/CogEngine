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
            CaminhoImagem = Configuracao.RetornarPastaArquivos() + "\\video-game-controller.jpg";
            Imagem = new System.Drawing.Bitmap(CaminhoImagem);
            Altura = 80;
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
                CaminhoImagem = new ManipuladorArquivo().SalvarImagem(value);
            }
        }

        [Browsable(false)]
        public string CaminhoImagem
        {
            get;
            set;
        }
    }
}
