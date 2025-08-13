using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas.Listar
{
    public sealed record Response(IList<Buscar.Response> Bibliotecas) : IQueryResponse;  
}
