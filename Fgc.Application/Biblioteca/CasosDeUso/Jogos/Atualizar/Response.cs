using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Jogos.Atualizar
{
    public sealed record Response(Guid Id, string Titulo, decimal Preco, int AnoLancamento, string Desenvolvedora, IList<Generos.Buscar.Response> Generos) : ICommandResponse;
}
