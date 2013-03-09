using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CogEngine.Objects
{
    public class Som
    {
        public string CaminhoCompleto { get; private set; }
        public string NomeArquivo
        {
            get
            {
                int i = CaminhoCompleto.LastIndexOf('\\');
                if (i >= 0)
                {
                    return CaminhoCompleto.Substring(i + 1);
                }
                else
                {
                    return "";
                }
            }
        }

        public Som(string caminhoCompleto)
        {
            CaminhoCompleto = caminhoCompleto;
        }

        public override string ToString()
        {
            return NomeArquivo;
        }
    }
}
