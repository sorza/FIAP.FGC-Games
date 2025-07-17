using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Jogos.Criar
{
    public sealed record Command(string titulo, decimal preco, DateTime dataLancamento, string desenvolvedora, IList<Generos.Buscar.Response> generos) : ICommand<Response>;
}
