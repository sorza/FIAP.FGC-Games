using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Generos.Buscar
{
    public record Response(Guid Id, string Genero) : IQueryResponse;
}
