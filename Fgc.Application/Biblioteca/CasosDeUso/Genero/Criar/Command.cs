using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Genero.Criar
{
    public sealed record Command(string nomeGenero) : ICommand<Response>;
}
