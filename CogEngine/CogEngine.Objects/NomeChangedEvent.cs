using System;

namespace CogEngine.Objects
{
    public class NomeChangedEvent : EventArgs
    {
        public string ID { get; private set; }
        public string NomeNovo { get; private set; }

        public NomeChangedEvent(string id, string nomeNovo)
        {
            ID = id;
            NomeNovo = nomeNovo;
        }
    }
}
