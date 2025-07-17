using Fgc.Domain.Biblioteca.Entidades;

namespace Fgc.Application.Compartilhado.Repositorios.Abstracoes
{
    public interface IGeneroRepository : IRepository<Genero>
    {
        Task<bool> VerificaSeGeneroExisteAsync(string genero, CancellationToken cancellationToken = default);
        Task<bool> VerificaSeGeneroExisteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
