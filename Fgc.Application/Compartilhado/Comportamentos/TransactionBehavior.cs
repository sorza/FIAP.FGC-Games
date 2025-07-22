using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Results;
using MediatR;

namespace Fgc.Application.Compartilhado.Comportamentos
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : Result
    where TRequest : notnull
    {
        private readonly IUnitOfWork _uow;

        public TransactionBehavior(IUnitOfWork uow)
            => _uow = uow;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken ct)
        {           
            await _uow.BeginTransactionAsync(ct);

            try
            {
                var response = await next();
                               
                if (response is Result r && r.IsSuccess)
                {
                    await _uow.SaveChangesAsync(ct);
                    await _uow.CommitAsync(ct);
                }
                else
                {
                    await _uow.RollbackAsync(ct);
                }

                return response;
            }
            catch
            {
                await _uow.RollbackAsync(ct);
                throw;
            }
        }
    }
}
