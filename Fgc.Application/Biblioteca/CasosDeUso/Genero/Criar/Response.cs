using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Genero.Criar
{
    public sealed record Response(Guid Id, string Genero) : ICommandResponse;
}
