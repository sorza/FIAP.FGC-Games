using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Generos.Listar
{
    public record Response(IList<Buscar.Response> generos) : IQueryResponse;
}
