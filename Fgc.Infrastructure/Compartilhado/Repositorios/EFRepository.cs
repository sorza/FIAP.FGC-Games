using Fgc.Application.Compartilhado.Repositorios;
using Fgc.Domain.Compartilhado.Entidades;
using Fgc.Infrastructure.Compartilhado.Data;
using Microsoft.EntityFrameworkCore;

namespace Fgc.Infrastructure.Compartilhado.Repositorios
{
    public class EFRepository<T>(AppDbContext context) : IRepository<T> where T : Entidade
    {
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        public Task Alterar(T entidade)
        {
            if (entidade is null) throw new ArgumentNullException(nameof(entidade));
            
            entidade.AtualizarDataAlteracao();

            _dbSet.Update(entidade);
            return context.SaveChangesAsync();
        }

        public Task Cadastrar(T entidade)
        {
            if (entidade is null) throw new ArgumentNullException(nameof(entidade));
            
            _dbSet.Add(entidade);
            return context.SaveChangesAsync();
        }

        public Task Deletar(Guid id)
        {
            var entidade = ObterPorId(id);

            _dbSet.Remove(entidade);
            return context.SaveChangesAsync();

        }

        public T ObterPorId(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("O ID não pode ser vazio.", nameof(id));
            var entidade = _dbSet.Find(id);
            if (entidade is null) throw new KeyNotFoundException($"Entidade com ID {id} não encontrada.");
            return entidade;
        }

        public IList<T> ObterTodos() => _dbSet.ToList();
        
    }
}
