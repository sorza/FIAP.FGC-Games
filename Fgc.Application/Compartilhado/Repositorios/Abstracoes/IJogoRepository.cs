using Fgc.Domain.Biblioteca.Entidades;

namespace Fgc.Application.Compartilhado.Repositorios.Abstracoes
{
    public interface IJogoRepository : IRepository<Jogo>
    {
        Task<bool> VerificaSeJogoExisteAsync(Jogo jogo, CancellationToken cancellationToken = default);
    }
}
