using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace CogEngine.Objects.WinForms
{
    public class TextoWinControl : System.Windows.Forms.Label, ICogEngineWinControl, ITexto
    {
        private ConcentradorTexto _Texto;

        public TextoWinControl(ConcentradorTexto concentradorTexto)
        {
            _Texto = concentradorTexto;
            Text = _Texto.Nome;
            AutoSize = true;
        }

        [Browsable(false)]
        public ConcentradorObjeto Objeto
        {
            get { return _Texto; }
        }

        public System.Windows.Forms.Control InitWinControl()
        {
            return GetControl();
        }

        public System.Windows.Forms.Control GetControl()
        {
            return this;
        }

        public string Texto
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
            }
        }

        public float TamanhoFonte
        {
            get
            {
                return Font.Size;
            }
            set
            {
                Font = new System.Drawing.Font(Font.FontFamily, value);
            }
        }

        public System.Drawing.Color Cor
        {
            get
            {
                return ForeColor;
            }
            set
            {
                ForeColor = value;
            }
        }

        public int PosicaoX
        {
            get
            {
                return Location.X;
            }
            set
            {
                Location = new System.Drawing.Point(value, Location.Y);
            }
        }

        public int PosicaoY
        {
            get
            {
                return Location.Y;
            }
            set
            {
                Location = new System.Drawing.Point(Location.X, value);
            }
        }

        [Browsable(false)]
        public string IDScript
        {
            get;
            set;
        }
    }
}
