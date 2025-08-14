using Fgc.Domain.Biblioteca.Exceptions;
using Fgc.Domain.Biblioteca.Exceptions.Biblioteca;
using Fgc.Domain.Compartilhado.Entidades;

namespace Fgc.Domain.Biblioteca.Entidades
{
    public class Biblioteca : Entidade
    {
        #region Campos
        private readonly List<BibliotecaJogo> _jogos = [];

        #endregion

        #region Construtores
        private Biblioteca() : base(Guid.NewGuid())
        {

        }

        private Biblioteca(Guid id, Guid contaId, string titulo) : base(id)
        {
            ContaId = contaId;
            Titulo = titulo;
        }

        #endregion

        #region Fábricas
        public static Biblioteca Criar(Guid contaId, string titulo)
        {
            if (contaId == Guid.Empty)
                throw new ContaIdVazioException(MensagemDeErro.Biblioteca.ContaIdVazio);

            if (string.IsNullOrWhiteSpace(titulo))
                throw new TituloNuloOuVazioException(MensagemDeErro.Biblioteca.TituloNuloOuVazio);

            return new Biblioteca(Guid.NewGuid(), contaId, titulo);
        }
        #endregion

        #region Propriedades
        public Guid ContaId { get; private set; }
        public string Titulo { get; private set; }
        public IReadOnlyCollection<BibliotecaJogo> Jogos => _jogos.AsReadOnly();       

        #endregion

        #region Métodos
        public void AdicionarJogo(Jogo jogo)
        {
            if(jogo is null)
                throw new JogoNuloException(MensagemDeErro.Biblioteca.JogoNulo);

            if (_jogos.Any(x => x.JogoId == jogo.Id))
                throw new JogoJaAdicionadoException(MensagemDeErro.Biblioteca.JogoJaAdicionado);

            _jogos.Add(BibliotecaJogo.Criar(this, jogo));
        }

        public void RemoverJogo(Guid id)
        {
            if (id == Guid.Empty)
                throw new JogoNuloException(MensagemDeErro.Biblioteca.JogoNulo);
            var jogo = _jogos.FirstOrDefault(x => x.JogoId == id);
            if (jogo is null)
                throw new JogoNuloException(MensagemDeErro.Biblioteca.JogoNaoEncontrado);
            _jogos.Remove(jogo);
        }

        public void AtualizarTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new TituloNuloOuVazioException(MensagemDeErro.Biblioteca.TituloNuloOuVazio);
            Titulo = titulo;
        }
        #endregion
    }
}
