namespace Fgc.Application.Compartilhado.Repositorios.Abstracoes
{
    public interface IBibliotecaRepository : IRepository<Domain.Biblioteca.Entidades.Biblioteca>
    {        
        Task<bool> VerificaSeBibliotecaExisteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
