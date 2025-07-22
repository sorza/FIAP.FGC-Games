using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Jogos.Criar
{
    public sealed record Command(string Titulo, decimal Preco, int AnoLancamento, string Desenvolvedora, IList<Generos.Buscar.Response> Generos) : ICommand<Response>;
}
