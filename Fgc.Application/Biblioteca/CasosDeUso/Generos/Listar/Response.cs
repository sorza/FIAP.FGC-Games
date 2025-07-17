using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Domain.Biblioteca.Entidades;

namespace Fgc.Application.Biblioteca.CasosDeUso.Generos.Listar
{
    public record Response(IList<Genero> generos) : IQueryResponse;
}
