using Fgc.Application.Compartilhado.Results;
using MediatR;

namespace Fgc.Application.Compartilhado.CasosDeUso.Abstracoes
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>> where TResponse : IQueryResponse;
}
