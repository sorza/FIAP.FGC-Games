using Fgc.Domain.Biblioteca.Entidades;

namespace Fgc.Application.Compartilhado.Repositorios.Abstracoes
{
    public interface IJogoRepository : IRepository<Jogo>
    {
        Task<bool> VerificaSeJogoExisteAsync(Jogo jogo, CancellationToken cancellationToken = default);
        Task<Jogo?> ObterPorIdComGeneros(Guid id, CancellationToken cancellationToken = default);
    }
}
