using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Jogos.Buscar
{
    public sealed record Query(Guid id) : IQuery<Response>;
}
    