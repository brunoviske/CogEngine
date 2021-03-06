﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CogEngine.Objects.WinForms;
using CogEngine.Objects.XNA;

namespace CogEngine.Objects
{
    public abstract class ConcentradorObjeto
    {
        protected abstract string Prefixo { get; }
        public Jogo Jogo { get; private set; }
        private string _Nome = null;
        public string Nome
        {
            get
            {
                return _Nome;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    if (PropriedadeInvalida != null)
                    {
                        PropriedadeInvalidaEvent evento = new PropriedadeInvalidaEvent("Nome", "O nome do objeto não pode ser vazio.");
                        PropriedadeInvalida(this, evento);
                    }
                }
                else if (Jogo.ListaObjeto.FirstOrDefault(c => c.Nome == value) == null)
                {
                    _Nome = value;
                    if (NomeAlterado != null)
                    {
                        NomeChangedEvent evento = new NomeChangedEvent(ID, _Nome);
                        NomeAlterado(this, evento);
                    }
                }
                else if (PropriedadeInvalida != null)
                {
                    PropriedadeInvalidaEvent evento = new PropriedadeInvalidaEvent("Nome", "Já existe um objeto com o nome informado!");
                    PropriedadeInvalida(this, evento);
                }
            }
        }
        public string ID { get; private set; }

        public PropriedadeInvalidaHandler PropriedadeInvalida;
        public NomeChangedEventHandler NomeAlterado;

        public void IniciarNome()
        {
            if (_Nome == null)
            {
                string nome;
                int i = 1;
                do
                {
                    nome = Prefixo + " " + i++;
                } while (Jogo.ListaObjeto.FirstOrDefault(c => c.Nome == nome) != null);
                _Nome = nome;
            }
        }

        public ConcentradorObjeto(Jogo jogo)
        {
            Jogo = jogo;
            Jogo.ListaObjeto.Add(this);
            ID = Guid.NewGuid().ToString();
        }

        public abstract ICogEngineWinControl WinControl { get; }
        public abstract ICogEngineXNAControl XNAControl { get; }
        public abstract Type BaseInterface { get; }
        public IObjetoScript Script { get; set; }

        ~ConcentradorObjeto()
        {
            Jogo.ListaObjeto.Remove(this);
        }

        internal void Remover()
        {
            Jogo.ListaObjeto.Remove(this);
        }
    }
}
