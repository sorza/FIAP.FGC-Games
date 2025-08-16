using Fgc.Domain.Biblioteca.Entidades;

namespace Fgc.Application.Compartilhado.Repositorios.Abstracoes
{
    public interface IBibliotecaJogoRepository : IRepository<BibliotecaJogo>
    {
        Task<bool> JogoEstaEmAlgumaBiblioteca(Guid jogoId, CancellationToken cancellationToken = default);       

    }
}
