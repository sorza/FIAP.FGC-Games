using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Generos.Atualizar
{
    public sealed record Command(Guid id, string nome) : ICommand<Response>;
}
