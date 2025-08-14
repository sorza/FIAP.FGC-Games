using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas.Listar
{
    public sealed record Response(IList<Criar.Response> Bibliotecas) : IQueryResponse;  
}
