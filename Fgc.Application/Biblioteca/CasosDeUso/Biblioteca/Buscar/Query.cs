using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Biblioteca.Buscar
{
    public record Query(string Id) : IQuery<Response>;
}
