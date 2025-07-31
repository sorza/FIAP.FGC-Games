namespace Fgc.Application.Compartilhado.Repositorios.Abstracoes
{
    internal interface IBibliotecaRepository : IRepository<Domain.Biblioteca.Entidades.Biblioteca>
    {        
        Task<bool> VerificaSeBibliotecaExisteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
