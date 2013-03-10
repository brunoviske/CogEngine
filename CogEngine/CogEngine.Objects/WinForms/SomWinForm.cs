using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CogEngine.Objects.WinForms
{
    public class SomWinForm : Som
    {
        public SomWinForm(string caminho)
        {
            CaminhoCompleto = new ManipuladorArquivo().SalvarArquivo(caminho);
        }
    }
}
