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
        public string SalvarImagem(Image image)
        {
            string caminhoImagem = CaminhoArquivo("Imagem");
            image.Save(caminhoImagem);
            return caminhoImagem;
        }

        public string SalvarArquivo(string arquivoOriginal)
        {
            string caminhoArquivo = CaminhoArquivo("Som_");
            File.Copy(arquivoOriginal, caminhoArquivo);
            return caminhoArquivo;
        }

        private string CaminhoArquivo(string prefixo)
        {
            int i = 1;
            prefixo = Configuracao.RetornarPastaArquivos() + "\\" + prefixo;
            string caminhoImagem;
            while (File.Exists((caminhoImagem = prefixo + i++))) ;
            return caminhoImagem;
        }
    }
}
