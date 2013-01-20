using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CogEngine.Objects.WinForms;
using CogEngine.Objects.XNA;

namespace CogEngine.Objects
{
    [Objeto("Texto", typeof(ConcentradorTexto))]
    public class ConcentradorTexto : ConcentradorObjeto
    {
        public ConcentradorTexto()
        {
            Nome = GetNome();
        }

        public override string Nome
        {
            get;
            set;
        }

        private TextoWinControl _TextoWinControl;
        public override ICogEngineWinControl WinControl
        {
            get
            {
                if (_TextoWinControl == null)
                {
                    _TextoWinControl = new TextoWinControl(this);
                }
                return _TextoWinControl;
            }
        }

        private TextoXNAControl _TextoXNAControl;
        public override ICogEngineXNAControl XNAControl
        {
            get
            {
                if (_TextoXNAControl == null)
                {
                    _TextoXNAControl = new TextoXNAControl(this);
                }
                return _TextoXNAControl;
            }
        }

        public override Type BaseInterface
        {
            get { return typeof(ITexto); }
        }

        public static int _Num = 1;
        private static string GetNome()
        {
            return "Texto " + _Num++;
        }
    }
}
