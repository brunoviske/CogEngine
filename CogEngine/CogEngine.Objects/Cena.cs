using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using CogEngine.Objects.XNA;
using System.ComponentModel;

namespace CogEngine.Objects
{
    public class Cena
    {
        protected List<ConcentradorObjeto> ListaObjeto { get; private set; }

        public virtual Color Cor { get; set; }
        public virtual string Nome { get; set; }
        public event AlteracaoListaEventHandler AlteracaoObjeto;

        public Cena()
        {
            ListaObjeto = new List<ConcentradorObjeto>();
        }

        public ConcentradorObjeto[] ListarObjetos()
        {
            return ListaObjeto.ToArray();
        }

        public virtual void AdicionarObjeto(ConcentradorObjeto item)
        {
            ListaObjeto.Add(item);
            Ordenar();
            if (AlteracaoObjeto != null)
            {
                AlteracaoObjeto(this, new AlteracaoListaEventArgs(item, TipoAlteracaoLista.Adicao));
            }
        }

        protected virtual int Comparar(ConcentradorObjeto x, ConcentradorObjeto y)
        {
            return x.XNAControl.ZIndex.CompareTo(y.XNAControl.ZIndex);
        }

        public void Ordenar()
        {
            ListaObjeto.Sort(Comparar);
        }

        public virtual void RemoverObjeto(ConcentradorObjeto item)
        {
            ListaObjeto.Remove(item);
            item.Remover();
            if (AlteracaoObjeto != null)
            {
                AlteracaoObjeto(this, new AlteracaoListaEventArgs(item, TipoAlteracaoLista.Remocao));
            }
        }
    }
}
