using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace CogEngine.Objects
{
    class ManipuladorArquivo
    {
        private Jogo _Jogo;

        public ManipuladorArquivo(Jogo jogo)
        {
            _Jogo = jogo;
        }

        public string SalvarImagem(Image image)
        {
            string caminhoImagem = CaminhoArquivo("Imagem", EstruturaProjeto.PastaImagem);
            image.Save(caminhoImagem);
            return caminhoImagem;
        }

        public string SalvarArquivo(string arquivoOriginal)
        {
            string caminhoArquivo = CaminhoArquivo("Som_", EstruturaProjeto.PastaSom);
            File.Copy(arquivoOriginal, caminhoArquivo);
            return caminhoArquivo;
        }

        private string CaminhoArquivo(string prefixo, EstruturaProjeto estrutura)
        {
            int i = 1;
            prefixo = _Jogo.RetornarPastaProjeto(estrutura) + prefixo;
            string caminhoImagem;
            while (File.Exists((caminhoImagem = prefixo + i++))) ;
            return caminhoImagem;
        }
    }
}
