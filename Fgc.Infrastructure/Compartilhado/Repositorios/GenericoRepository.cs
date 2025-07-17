using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Domain.Compartilhado.Entidades;
using Fgc.Infrastructure.Compartilhado.Data;
using Microsoft.EntityFrameworkCore;

namespace Fgc.Infrastructure.Compartilhado.Repositorios
{
    public class GenericoRepository<T>(AppDbContext context) : IRepository<T> where T : Entidade
    {
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        public Task Alterar(T entidade, CancellationToken cancellationToken = default)
        {        
            _dbSet.Update(entidade);
            return context.SaveChangesAsync(cancellationToken);
        }

        public Task Cadastrar(T entidade, CancellationToken cancellationToken = default)
        {            
            _dbSet.Add(entidade);
            return context.SaveChangesAsync(cancellationToken);
        }

        public Task Deletar(Guid id, CancellationToken cancellationToken = default)
        {
            var entidade = ObterPorId(id).Result;

            _dbSet.Remove(entidade!);
            return context.SaveChangesAsync(cancellationToken);
        }

        public async Task<T?> ObterPorId(Guid id, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        public Task<IList<T>> ObterTodos(CancellationToken cancellationToken = default)
            => _dbSet.ToListAsync(cancellationToken).ContinueWith(task => (IList<T>)task.Result, cancellationToken);        
        
    }
}
