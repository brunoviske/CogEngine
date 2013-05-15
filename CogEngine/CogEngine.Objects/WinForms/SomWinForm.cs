using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CogEngine.Objects.WinForms
{
    public class SomWinForm : Som
    {
        public SomWinForm(Jogo jogo) : base(jogo) { }

        public void CopiarArquivo(string caminho)
        {
            CaminhoRelativo = Jogo.RetornarCaminhoRelativo(EstruturaProjeto.PastaSom, new ManipuladorArquivo(Jogo).SalvarArquivo(caminho));
        }
    }
}
