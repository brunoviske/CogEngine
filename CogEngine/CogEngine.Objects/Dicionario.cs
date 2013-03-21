using System.Collections.Generic;

namespace CogEngine.Objects
{
    public class Dicionario<TKey, TValue> : Dictionary<TKey, TValue>
    {
        private void AdicionarChave(TKey chave)
        {
            if (!ContainsKey(chave))
            {
                Add(chave, default(TValue));
            }
        }

        public new TValue this[TKey key]
        {
            get
            {
                AdicionarChave(key);
                return base[key];
            }
            set
            {
                AdicionarChave(key);
                base[key] = value;
            }
        }
    }
}
