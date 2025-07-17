using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Jogos.Listar
{
    public record Response(IList<Buscar.Response> Jogos) : IQueryResponse;
}
