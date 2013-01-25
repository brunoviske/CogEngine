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
        private FiguraObjeto _Figura;

        public FiguraXNAControl(FiguraObjeto figura)
        {
            _Figura = figura;
        }

        public override string CaminhoImagem
        {
            get
            {
                return _Figura.CaminhoArquivo;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
