using Fgc.Domain.Compartilhado.Entidades;

namespace Fgc.Application.Compartilhado.Repositorios
{
    public interface IRepository<T> where T : Entidade
    {
        IList<T> ObterTodos();
        IList<T> ObterPorId(Guid id);
        Task Cadastrar(T entidade);
        Task Alterar(T entidade);
        Task Deletar(Guid id);
    }
}
