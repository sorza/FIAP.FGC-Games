﻿namespace Fgc.Application.Compartilhado.Repositorios.Abstracoes
{
    public interface IUnitOfWork
    {       
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);      
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task CommitAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}
