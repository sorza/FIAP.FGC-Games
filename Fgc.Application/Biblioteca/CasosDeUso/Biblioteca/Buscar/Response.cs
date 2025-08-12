using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Biblioteca.Buscar
{
    public record Response(Guid Id, Guid ContaId, string Titulo) : IQueryResponse;
}
