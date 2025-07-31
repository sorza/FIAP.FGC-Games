using Fgc.Domain.Biblioteca.Exceptions;
using Fgc.Domain.Biblioteca.Exceptions.Biblioteca;
using Fgc.Domain.Compartilhado.Entidades;
using Fgc.Domain.Usuario.Entidades;

namespace Fgc.Domain.Biblioteca.Entidades
{
    public class Biblioteca : Entidade
    {
        #region Campos
        public IReadOnlyCollection<BibliotecaJogo> Jogos => _jogos.AsReadOnly();

        #endregion

        #region Construtores
        private Biblioteca() : base(Guid.NewGuid())
        {

        }

        private Biblioteca(Guid id, Guid contaId) : base(id)
        {
            ContaId = contaId;
        }

        #endregion

        #region Fábricas
        public static Biblioteca Criar(Guid contaId)
        {
            if (contaId == Guid.Empty)
                throw new ContaIdVazioException(MensagemDeErro.Biblioteca.ContaIdVazio);
            return new Biblioteca(Guid.NewGuid(), contaId);
        }
        #endregion

        #region Propriedades
        public Guid ContaId { get; private set; }
        public Conta Conta { get; private set; }

        private readonly List<BibliotecaJogo> _jogos = [];

        #endregion

        #region Métodos
        public void AdicionarJogo(Jogo jogo)
        {
            if(jogo is null)
                throw new JogoNuloException(MensagemDeErro.Biblioteca.JogoNulo);

            if (_jogos.Any(x => x.JogoId == jogo.Id))
                throw new JogoJaAdicionadoException(MensagemDeErro.Biblioteca.JogoJaAdicionado);

            _jogos.Add(BibliotecaJogo.Criar(this, jogo, DateTime.UtcNow));
        }
        #endregion
    }
}
