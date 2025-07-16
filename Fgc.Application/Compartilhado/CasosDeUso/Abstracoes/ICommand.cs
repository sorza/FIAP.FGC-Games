using Fgc.Application.Compartilhado.Results;
using MediatR;

namespace Fgc.Application.Compartilhado.CasosDeUso.Abstracoes
{
    public interface ICommand : IRequest<Result>;

    public interface ICommand<TCommandResponse> : IRequest<Result<TCommandResponse>>
        where TCommandResponse : ICommandResponse;
}
