using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Domain.Biblioteca.Entidades;

namespace Fgc.Application.Biblioteca.CasosDeUso.Jogos.Criar
{
    public sealed record Command(string titulo, decimal preco, DateTime dataLancamento, string desenvolvedora, List<Genero> generos) : ICommand<Response>;
}
