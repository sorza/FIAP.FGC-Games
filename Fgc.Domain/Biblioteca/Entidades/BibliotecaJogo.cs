using Fgc.Domain.Biblioteca.Exceptions;
using Fgc.Domain.Biblioteca.Exceptions.Biblioteca;
using Fgc.Domain.Compartilhado.Entidades;

namespace Fgc.Domain.Biblioteca.Entidades
{
    public class BibliotecaJogo : Entidade
    {
        #region Construtores
        private BibliotecaJogo() : base (Guid.NewGuid()){ }

        private BibliotecaJogo(Guid id, Biblioteca biblioteca, Jogo jogo) : base(id)
        {
            Biblioteca = biblioteca;
            BibliotecaId = biblioteca.Id;
            Jogo = jogo;
            JogoId = jogo.Id;
            DataAquisicao = DateTime.Now;
            ValorPago = jogo.Preco;
        }
        #endregion

        #region Fábricas

        public static BibliotecaJogo Criar(Biblioteca biblioteca, Jogo jogo)
        {
            if (biblioteca is null)
                throw new BibliotecaNulaException(MensagemDeErro.Biblioteca.BibliotecaNula);
            if (jogo is null)
                throw new JogoNuloException(MensagemDeErro.Biblioteca.JogoNulo);
            return new BibliotecaJogo(Guid.NewGuid(), biblioteca, jogo);
        }

        #endregion

        #region Propriedades
        public Guid BibliotecaId { get; private set; }
        public Biblioteca Biblioteca { get; private set; }

        public Guid JogoId { get; private set; }
        public Jogo Jogo { get; private set; }
       
        public DateTime DataAquisicao { get; private set; }
        public decimal ValorPago { get; set; }

        #endregion
       
    }
}