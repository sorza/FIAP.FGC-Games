using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Biblioteca.Listar
{
    public sealed record Response(IList<Buscar.Response> Bibliotecas) : IQueryResponse;  
}
