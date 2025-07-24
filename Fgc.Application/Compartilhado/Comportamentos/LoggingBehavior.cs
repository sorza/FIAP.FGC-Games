using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Diagnostics;

namespace Fgc.Application.Compartilhado.Comportamentos
{
    public class LoggingBehavior<TRequest, TResponse>
       : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>

    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            using (LogContext.PushProperty("RequestName", typeof(TRequest).Name))
            {
                var timer = Stopwatch.StartNew();

                // 2) Log de início
                _logger.LogInformation(
                  "Processando a requisicao {RequestName} {@Request}",
                  typeof(TRequest).FullName,
                  request);

                try
                {
                    var response = await next();
                    timer.Stop();
                  
                    _logger.LogInformation(
                      "A requisicao {RequestName} foi processada em {ElapsedMilliseconds}ms",
                      typeof(TRequest).Name,
                      timer.ElapsedMilliseconds);

                    return response;
                }
                catch (Exception ex)
                {
                    timer.Stop();
                   
                    _logger.LogError(
                      ex,
                      "A requisicao {RequestName} falhou depois de {ElapsedMilliseconds}ms",
                      typeof(TRequest).Name,
                      timer.ElapsedMilliseconds);

                    throw;
                }
            }

        }
    }
}
