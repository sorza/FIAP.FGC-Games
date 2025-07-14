using Fgc.Domain.Biblioteca.Exceptions;
using Fgc.Domain.Biblioteca.Exceptions.Jogo;
using Fgc.Domain.Compartilhado.Entidades;

namespace Fgc.Domain.Biblioteca.Entidades
{
    public class Jogo : Entidade
    {
        private readonly List<Genero> _generos = [];

        #region Construtores
        private Jogo() : base(Guid.NewGuid())
        {
            
        }

        private Jogo(Guid id, string titulo, decimal preco, DateTime dataLancamento, string desenvolvedora, List<Genero> generos) : base(id)
        {
            Titulo = titulo;
            Preco = preco;
            DataLancamento = dataLancamento;
            _generos = generos;
            Desenvolvedora = desenvolvedora;
        }
        #endregion

        #region Propriedades
        public string Titulo { get; private set; } = string.Empty;      
        public decimal Preco { get; private set; }
        public DateTime DataLancamento { get; private set; }
        public IReadOnlyCollection<Genero> Generos => _generos;
        public string Desenvolvedora { get; private set; } = string.Empty;

        #endregion

        #region Fábricas

        public static Jogo Criar(string titulo, decimal preco, DateTime dataLancamento, string desenvolvedora, List<Genero> generos)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new TituloNuloOuVazioException(MensagemDeErro.Jogo.TituloNuloOuVazio);
            if (preco < 0)
                throw new PrecoNegativoException(MensagemDeErro.Jogo.PrecoNegativo);
            if (dataLancamento > DateTime.UtcNow)
                throw new DataLancamentoFuturaException(MensagemDeErro.Jogo.DataLancamentoFutura);
            if (generos == null || generos.Count == 0)
                throw new GeneroObrigatorioException(MensagemDeErro.Jogo.GeneroObrigatorio);
            if (string.IsNullOrWhiteSpace(desenvolvedora))
                throw new DesenvolvedoraNulaOuVaziaException(MensagemDeErro.Jogo.DesenvolvedoraNulaOuVazia);
            return new Jogo(Guid.NewGuid(), titulo, preco, dataLancamento, desenvolvedora, generos);
        }

        #endregion

        #region Métodos
        public void AdicionarGenero(Genero genero)
        {
            if (_generos.Contains(genero)) return;
            _generos.Add(genero);
            AtualizarDataAlteracao();
        }

        public void RemoverGenero(Genero genero)
        {
            if (!_generos.Contains(genero)) return;
            _generos.Remove(genero);
            AtualizarDataAlteracao();
        }

        #endregion

    }
}
