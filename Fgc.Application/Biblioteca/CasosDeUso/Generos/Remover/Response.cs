using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Generos.Remover
{
    public sealed record Response(Guid Id) : ICommandResponse;
}
