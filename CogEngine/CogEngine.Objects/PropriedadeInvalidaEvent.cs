using System;

namespace CogEngine.Objects
{
    public class PropriedadeInvalidaEvent : EventArgs
    {
        public string NomePropriedade { get; private set; }
        public string MensagemErro { get; private set; }

        internal PropriedadeInvalidaEvent(string propriedade, string mensagemErro)
        {
            NomePropriedade = propriedade;
            MensagemErro = mensagemErro;
        }
    }
}
