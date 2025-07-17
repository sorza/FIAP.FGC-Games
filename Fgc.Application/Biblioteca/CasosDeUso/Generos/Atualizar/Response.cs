using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Generos.Atualizar
{
    public sealed record Response(Guid Id, string Genero) : ICommandResponse;
}
