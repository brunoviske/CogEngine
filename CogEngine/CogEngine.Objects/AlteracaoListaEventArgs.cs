using System;

namespace CogEngine.Objects
{
    public enum TipoAlteracaoLista
    {
        Adicao,
        Remocao
    }

    public class AlteracaoListaEventArgs : EventArgs
    {
        public object Objeto { get; private set; }
        public TipoAlteracaoLista TipoAlteracao { get; private set; }

        public AlteracaoListaEventArgs(object objeto, TipoAlteracaoLista tipoAlteracao)
        {
            Objeto = objeto;
            TipoAlteracao = tipoAlteracao;
        }
    }
}
