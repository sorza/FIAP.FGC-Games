using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Biblioteca.Criar
{
    public sealed record Response(Guid Id, Guid ContaId, string Titulo) : ICommandResponse;
}
