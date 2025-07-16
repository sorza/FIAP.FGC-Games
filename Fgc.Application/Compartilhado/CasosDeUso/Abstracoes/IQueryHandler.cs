using Fgc.Application.Compartilhado.Results;
using MediatR;

namespace Fgc.Application.Compartilhado.CasosDeUso.Abstracoes
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
        where TResponse : IQueryResponse;
}
