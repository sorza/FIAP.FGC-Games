using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas.Buscar
{
    public record Response(Guid Id, Guid ContaId, string Titulo, IList<AdicionarJogo.Response> Jogos) : IQueryResponse;
}
