using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Generos.Buscar
{    
    public record Query(string Id) : IQuery<Response>;
}
