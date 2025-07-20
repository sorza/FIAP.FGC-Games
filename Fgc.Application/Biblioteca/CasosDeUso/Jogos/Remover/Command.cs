using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Jogos.Remover
{
    public sealed record Command(string Id) : ICommand<Response>;
}
