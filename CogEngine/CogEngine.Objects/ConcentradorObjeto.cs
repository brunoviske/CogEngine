using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CogEngine.Objects.WinForms;
using CogEngine.Objects.XNA;

namespace CogEngine.Objects
{
    public abstract class ConcentradorObjeto
    {
        private static List<ConcentradorObjeto> _Lista = new List<ConcentradorObjeto>();
        protected abstract string Prefixo { get; }
        private string _Nome = null;
        public string Nome
        {
            get
            {
                return _Nome;
            }
            set
            {
                if (_Lista.FirstOrDefault(c => c.Nome == value) == null)
                {
                    _Nome = value;
                }
                else
                {
                    throw new Exception("Já existe um objeto com o nome informado!");
                }
            }
        }

        public void IniciarNome()
        {
            if (_Nome == null)
            {
                int i = 1;
                do
                {
                    _Nome = Prefixo + i++;
                } while (_Lista.FirstOrDefault(c => c.Nome == null) != null);
            }
        }

        public ConcentradorObjeto()
        {
            _Lista.Add(this);
        }

        public abstract ICogEngineWinControl WinControl { get; }
        public abstract ICogEngineXNAControl XNAControl { get; }
        public abstract Type BaseInterface { get; }
        public IObjetoScript Script { get; set; }
        ~ConcentradorObjeto()
        {
            _Lista.Remove(this);
        }
    }
}
