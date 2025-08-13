using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Biblioteca.Atualizar
{
    public sealed record Command(string Id, string Titulo) : ICommand<Response>;   
}
