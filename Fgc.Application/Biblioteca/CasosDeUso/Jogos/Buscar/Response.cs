using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Domain.Biblioteca.Entidades;

namespace Fgc.Application.Biblioteca.CasosDeUso.Jogos.Buscar
{
    public sealed record Response(Guid Id, string Titulo, decimal Preco, DateTime DataLancamento, string Desenvolvedora, List<Genero> Generos) : IQueryResponse;
}
