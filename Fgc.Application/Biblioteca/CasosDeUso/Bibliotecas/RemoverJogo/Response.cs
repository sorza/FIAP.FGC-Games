using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas.RemoverJogo
{
    public sealed record Response(Guid BibliotecaId, Guid JogoId) : ICommandResponse;       
}
