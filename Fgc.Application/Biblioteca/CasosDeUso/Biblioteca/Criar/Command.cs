using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Biblioteca.Criar
{
    public sealed record Command(Guid ContaId, string Titulo) : ICommand<Response>;
    
}
