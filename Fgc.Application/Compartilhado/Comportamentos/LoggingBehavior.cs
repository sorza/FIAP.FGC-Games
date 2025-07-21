using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Fgc.Application.Compartilhado.Comportamentos
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<TRequest> logger)
       : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                logger.LogInformation($"Executando requisição: {request.GetType().Name}");
                var result = await next();

                stopwatch.Stop();

                logger.LogInformation($"Requisição {request.GetType().Name} processada em {stopwatch.ElapsedMilliseconds}ms.");
                return result;
            }
            catch (Exception e)
            {
                stopwatch.Stop();
                logger.LogError(e, $"Ocorreu um erro durante o processamento da requisição: {request.GetType().Name} depois de {stopwatch.ElapsedMilliseconds}ms");
                throw;
            }
        }
    }
}
