using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Domain.Biblioteca.Entidades;

namespace Fgc.Application.Biblioteca.CasosDeUso.Jogos.Criar
{
    public sealed record Response(Guid id, string titulo, decimal preco, int anoLancamento, string desenvolvedora, IList<Generos.Buscar.Response> generos) : ICommandResponse;
}
