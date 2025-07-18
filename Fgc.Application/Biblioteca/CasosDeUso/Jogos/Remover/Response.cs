using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Jogos.Remover
{
    public sealed record Response(Guid id) : ICommandResponse;
}
