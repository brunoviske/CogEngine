using System;
using System.Configuration;
using System.Linq;

namespace CogEngine.Objects
{
    static class Configuracao
    {
        public static string PastaArquivos
        {
            get
            {
                const string chaveConfig = "CaminhoRelativoPastaArquivos";
                string caminhoRelativo;
                if (ConfigurationManager.AppSettings.AllKeys.Contains(chaveConfig))
                {
                    caminhoRelativo = ConfigurationManager.AppSettings[chaveConfig];
                    if (!caminhoRelativo.EndsWith("\\")) caminhoRelativo += '\\';
                }
                else
                {
                    caminhoRelativo = "..\\..\\..\\Arquivos\\";
                }
                return caminhoRelativo;
            }
        }

        public static string PastaTemp
        {
            get
            {
                return PastaArquivos + "Temp\\";
            }
        }

        public static string PastaJogoTemplate
        {
            get
            {
                return PastaArquivos + "Jogo Template\\";
            }
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
    }
}
