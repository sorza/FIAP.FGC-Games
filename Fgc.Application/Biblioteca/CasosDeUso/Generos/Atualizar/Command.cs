using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Generos.Atualizar
{
    public sealed record Command(string id, string nome) : ICommand<Response>;
}
