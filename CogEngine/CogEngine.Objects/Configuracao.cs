using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CogEngine.Objects
{
    public static class Configuracao
    {
        private static string Prefixo;

        public static string RetornarPastaImagens()
        {
            return RetornarCaminhoAbsoluto(Prefixo + "Imagens");
        }

        public static string RetornarArquivoJogo()
        {
            return RetornarPastaTemp() + "\\jogo.xml";
        }

        public static string RetornarPastaTemp()
        {
            return RetornarCaminhoAbsoluto(Prefixo + "Temp");
        }

        public static string RetornarCaminhoCompilador()
        {
            return @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe";
        }

        private static string RetornarCaminhoAbsoluto(string caminhoRelativo)
        {
            return new DirectoryInfo(caminhoRelativo).FullName;
        }

        public static string RetornarReferenciaCogEngine()
        {
            return new FileInfo(Prefixo + "Referencias\\CogEngine.Objects.dll").FullName;
        }

        public static string RetornarPastaXNA()
        {
            string s = Environment.GetEnvironmentVariable("XNAGSv4");
            if (s != null)
            {
                if (!s.EndsWith("\\")) s += "\\";
                return s + @"References\Windows\x86";
            }
            else
            {
                return null;
            }
        }

        public static void Iniciar(Plataforma plataforma)
        {
            if (plataforma == Plataforma.Forms)
                Prefixo = "..\\..\\..\\";
            else if (plataforma == Plataforma.XNA)
                Prefixo = "..\\..\\..\\..\\..\\";
        }
    }
}
