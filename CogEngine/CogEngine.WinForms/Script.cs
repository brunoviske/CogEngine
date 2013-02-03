using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CogEngine.WinForms
{
    public class Script
    {
        public Script()
        {
            ID = Guid.NewGuid().ToString();
            NomeClasse = GetNome();
        }

        public string ID { get; set; }
        public string NomeAmigavel { get; set; }
        public string NomeClasse { get; private set; }
        public string CodigoScript { get; set; }
        public override string ToString()
        {
            return NomeAmigavel;
        }

        private static int _Num = 1;
        public static string GetNome()
        {
            return "Script_" + _Num++;
        }
    }
}
