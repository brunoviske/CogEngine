using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CogEngine.Objects
{
    public class Cena
    {
        private List<ConcentradorObjeto> _ListaObjeto;

        public virtual Color Cor { get; set; }

        public virtual string Nome { get; set; }

        public Cena()
        {
            _ListaObjeto = new List<ConcentradorObjeto>();
        }

        public virtual void AdicionarObjeto(ConcentradorObjeto item)
        {
            _ListaObjeto.Add(item);
        }

        public ConcentradorObjeto[] ListarObjetos()
        {
            return _ListaObjeto.ToArray();
        }
    }
}
