using Fgc.Domain.Compartilhado.Entidades;

namespace Fgc.Application.Compartilhado.Repositorios.Abstracoes
{
    public interface IRepository<T> where T : Entidade
    {
        Task<IList<T>> ObterTodos(CancellationToken cancellationToken = default);
        Task<T?> ObterPorId(Guid id, CancellationToken cancellationToken = default);
        Task Cadastrar(T entidade, CancellationToken cancellationToken = default);
        Task Alterar(T entidade, CancellationToken cancellationToken = default);
        Task Deletar(Guid id, CancellationToken cancellationToken = default);
        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}
