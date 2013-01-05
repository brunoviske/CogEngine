using System;

namespace CogEngine.Objects
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ObjetoAttribute : Attribute
    {
        public string TituloAmigavel { get; private set; }
        public Type TipoRelacionado { get; private set; }

        public ObjetoAttribute(string tituloAmigavel, Type type)
        {
            TituloAmigavel = tituloAmigavel;
            TipoRelacionado = type;
        }

        public override string ToString()
        {
            return TituloAmigavel;
        }
    }
}
