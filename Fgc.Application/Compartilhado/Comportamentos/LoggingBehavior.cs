using Fgc.Application.Compartilhado.Services;
using MediatR;
using Newtonsoft.Json;

namespace Fgc.Application.Compartilhado.Comportamentos
{
    public class LoggingBehavior<TRequest, TResponse>
       : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogService _logService;

        public LoggingBehavior(ILogService logService)
        {
            _logService = logService;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var inicio = DateTime.UtcNow;

            // Log de entrada
            await _logService.LogAsync(new LogEntry
            {
                Tipo = "Entrada",
                Timestamp = inicio,
                Dados = JsonConvert.SerializeObject(request)
            });

            var response = await next();

            // Log de saída
            await _logService.LogAsync(new LogEntry
            {
                Tipo = "Saída",
                Timestamp = DateTime.UtcNow,
                Dados = JsonConvert.SerializeObject(response)
            });

            return response;
        }

    }
    public sealed class LogEntry
    {
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;
        public string CorrelationId { get; init; } = Guid.NewGuid().ToString();
        public string UserId { get; init; } = string.Empty;
        public string HttpMethod { get; init; } = string.Empty;
        public string Endpoint { get; init; } = string.Empty;
        public object Dados { get; init; } = string.Empty;
        public string Resultado { get; init; } = string.Empty;
        public string Mensagem { get; init; } = string.Empty;
    }
}
