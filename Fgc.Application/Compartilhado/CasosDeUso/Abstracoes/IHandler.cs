namespace Fgc.Application.Compartilhado.CasosDeUso.Abstracoes
{
    public interface IHandler<TCommand, TResponse>
        where TCommand : ICommand
        where TResponse : IResponse
    {
        Task<TResponse> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    }
}
