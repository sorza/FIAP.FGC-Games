using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas.Remover
{
    public sealed record Response(Guid id) : ICommandResponse;
}
