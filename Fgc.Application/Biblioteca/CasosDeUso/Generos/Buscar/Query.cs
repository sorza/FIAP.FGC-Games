using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Generos.Buscar
{    
    public record Query(Guid Id) : IQuery<Response>;
}
