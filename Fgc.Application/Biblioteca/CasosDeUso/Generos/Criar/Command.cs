using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Generos.Criar
{
    public sealed record Command(string Genero) : ICommand<Response>;
}
