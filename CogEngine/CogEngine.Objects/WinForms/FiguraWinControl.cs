using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.ComponentModel;
using CogEngine.Objects.XNA;
using Microsoft.Xna.Framework;
using System.Drawing;
using System.IO;

namespace CogEngine.Objects.WinForms
{
    public class FiguraWinControl : AbstractImageControl
    {
        private FiguraObjeto _FiguraObjeto;

        internal FiguraWinControl(FiguraObjeto objeto)
        {
            _FiguraObjeto = objeto;
            Image = new System.Drawing.Bitmap(_FiguraObjeto.CaminhoArquivo);
        }

        [Browsable(false)]
        public override ConcentradorObjeto Objeto
        {
            get { return _FiguraObjeto; }
        }
    }
}
