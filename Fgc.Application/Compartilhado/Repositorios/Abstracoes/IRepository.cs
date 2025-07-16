using Fgc.Domain.Compartilhado.Entidades;

namespace Fgc.Application.Compartilhado.Repositorios.Abstracoes
{
    public interface IRepository<T> where T : Entidade
    {
        IList<T> ObterTodos();
        T ObterPorId(Guid id);
        Task Cadastrar(T entidade);
        Task Alterar(T entidade);
        Task Deletar(Guid id);
    }
}
