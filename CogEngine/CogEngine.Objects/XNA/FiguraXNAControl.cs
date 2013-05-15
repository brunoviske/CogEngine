using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;
using Microsoft.Xna.Framework.Content;

namespace CogEngine.Objects.XNA
{
    public class FiguraXNAControl : AbstractImagemXNAControl, IFigura
    {
        private FiguraObjeto Figura
        {
            get
            {
                return (FiguraObjeto)Objeto;
            }
        }

        private string _CaminhoImagem;

        public FiguraXNAControl(FiguraObjeto figura) : base(figura)
        {
            _CaminhoImagem = Jogo.RetornarArquivoPadrao(Figura.Arquivo);
        }

        public override string CaminhoImagem
        {
            get
            {
                return _CaminhoImagem;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
