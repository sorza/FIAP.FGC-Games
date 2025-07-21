using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Fgc.Application.Compartilhado.Comportamentos
{
    public class LoggingBehavior<TRequest, TResponse>
       : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>

    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                _logger.LogInformation($"Executando requisição: {request.GetType().FullName}");
                var result = await next();

                stopwatch.Stop();

                _logger.LogInformation($"Requisição {request.GetType().FullName} processada em {stopwatch.ElapsedMilliseconds}ms.");
                return result;
            }
            catch (Exception e)
            {
                stopwatch.Stop();
                _logger.LogError(e, $"Ocorreu um erro durante o processamento da requisição: {request.GetType().FullName} depois de {stopwatch.ElapsedMilliseconds}ms");
                throw;
            }
        }
    }
}
