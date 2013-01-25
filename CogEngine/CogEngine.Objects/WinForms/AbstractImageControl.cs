using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace CogEngine.Objects.WinForms
{
    public abstract class AbstractImageControl : PictureBox, ICogEngineWinControl, IFigura
    {
        public abstract ConcentradorObjeto Objeto { get; }

        public Control InitWinControl()
        {
            int x = Convert.ToInt32(PosicaoX);
            int y = Convert.ToInt32(PosicaoY);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager();
            Location = new System.Drawing.Point(x, y);
            SizeMode = PictureBoxSizeMode.StretchImage;
            Name = "pictureBox1";
            TabIndex = 0;
            TabStop = false;
            return GetControl();
        }

        public int Altura
        {
            get
            {
                return Size.Height;
            }
            set
            {
                Size = new System.Drawing.Size(Size.Width, value);
            }
        }

        public int Largura
        {
            get
            {
                return Size.Width;
            }
            set
            {
                Size = new System.Drawing.Size(value, Size.Height);
            }
        }

        public float PosicaoX
        {
            get
            {
                return Location.X;
            }
            set
            {
                Location = new System.Drawing.Point(Convert.ToInt32(value), Location.Y);
            }
        }

        public float PosicaoY
        {
            get
            {
                return Location.Y;
            }
            set
            {
                Location = new System.Drawing.Point(Location.X, Convert.ToInt32(value));
            }
        }

        public Control GetControl()
        {
            return this;
        }

        [Browsable(false)]
        public string IDScript
        {
            get;
            set;
        }
    }
}
