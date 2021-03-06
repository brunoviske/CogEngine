﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace CogEngine.Objects.WinForms
{
    public class CenaWinForm : Cena
    {
        [Browsable(false)]
        public Panel Painel { get; private set; }

        public event NomeChangedEventHandler OnNomeChanged;

        public override Color Cor
        {
            get
            {
                return Painel.BackColor;
            }
            set
            {
                Painel.BackColor = value;
            }
        }

        string _Nome;
        public override string Nome
        {
            get
            {
                return _Nome;
            }
            set
            {
                _Nome = value;
                if (OnNomeChanged != null)
                {
                    NomeChangedEvent e = new NomeChangedEvent(ID, _Nome);
                    OnNomeChanged(this, e);
                }
            }
        }

        [Browsable(false)]
        public string ID { get; private set; }

        public CenaWinForm()
            : base()
        {
            Painel = new Panel();
            Cor = System.Drawing.Color.CornflowerBlue;
            ID = Guid.NewGuid().ToString();
        }

        public override void AdicionarObjeto(ConcentradorObjeto item)
        {
            Painel.Controls.Add((Control)item.WinControl);
            base.AdicionarObjeto(item);
        }

        public override void RemoverObjeto(ConcentradorObjeto item)
        {
            Painel.Controls.Remove((Control)item.WinControl);
            base.RemoverObjeto(item);
        }

        public void CarregarPainel()
        {
            Painel.Controls.Clear();
            for (int i = ListaObjeto.Count - 1; i >= 0; i--)
            {
                Painel.Controls.Add((Control)ListaObjeto[i].WinControl);
            }
        }

        protected override int Comparar(ConcentradorObjeto x, ConcentradorObjeto y)
        {
            return x.WinControl.ZIndex.CompareTo(y.WinControl.ZIndex);
        }
    }
}
