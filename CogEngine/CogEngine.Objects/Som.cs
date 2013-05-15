using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CogEngine.Objects
{
    public abstract class Som
    {
        protected Jogo Jogo { get; private set; }

        public Som(Jogo jogo)
        {
            Jogo = jogo;
        }

        public virtual string CaminhoRelativo { get; set; }
        public string Nome { get; set; }
        public string CaminhoAbsoluto
        {
            get
            {
                return Jogo.RetornarCaminhoAbsoluto(EstruturaProjeto.PastaSom, CaminhoRelativo);
            }
        }
        public override string ToString()
        {
            return Nome;
        }
    }
}
