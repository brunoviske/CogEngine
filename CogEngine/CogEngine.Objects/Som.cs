using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CogEngine.Objects
{
    public abstract class Som
    {
        public virtual string CaminhoCompleto { get; set; }
        public string Nome { get; set; }
        public override string ToString()
        {
            return Nome;
        }
    }
}
