using Fgc.Application.Compartilhado.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Fgc.Application.Compartilhado.Comportamentos
{
    public class LoggingBehavior<TRequest, TResponse>
       : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogService _logger;

        public LoggingBehavior(IHttpContextAccessor httpContextAccessor, ILogService logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<TResponse> Handle(
         TRequest request,
         RequestHandlerDelegate<TResponse> next,
         CancellationToken cancellationToken)
        {
            var context = _httpContextAccessor.HttpContext;

            var correlationId = context?.Items["CorrelationId"]?.ToString() ?? Guid.NewGuid().ToString();
            var userId = context?.User.FindFirst("UsuarioId")?.Value;
            var httpMethod = context?.Request.Method;
            var endpoint = context?.Request.Path;

            await _logger.LogAsync(new LogEntry
            {
                CorrelationId = correlationId,
                UserId = userId,
                HttpMethod = httpMethod,
                Endpoint = endpoint,
                Dados = SanitizeToBson(request),
                Resultado = "Recebido"
            });

            try
            {
                var response = await next();

                await _logger.LogAsync(new LogEntry
                {
                    CorrelationId = correlationId,
                    UserId = userId,
                    HttpMethod = httpMethod,
                    Endpoint = endpoint,
                    Dados = SanitizeToBson(response),
                    Resultado = "Sucesso"
                });

                return response;
            }
            catch (Exception ex)
            {
                await _logger.LogAsync(new LogEntry
                {
                    CorrelationId = correlationId,
                    UserId = userId,
                    HttpMethod = httpMethod,
                    Endpoint = endpoint,
                    Dados = new BsonDocument { { "Erro", ex.Message } },
                    Resultado = "Erro"
                });

                throw;
            }
        }

        private BsonDocument SanitizeToBson(object obj)
        {
            if (obj == null) return new BsonDocument();

            if (obj is BsonDocument bdoc) return bdoc;

            var json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = new MaskSensitiveDataResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });

            var trimmed = json?.TrimStart();
            if (!string.IsNullOrWhiteSpace(trimmed) && trimmed.StartsWith("{"))
            {               
                return BsonDocument.Parse(json);
            }
           
            var value = JsonConvert.DeserializeObject<object>(json);
            return new BsonDocument("value", BsonValue.Create(value));
        }

        private class MaskSensitiveDataResolver : DefaultContractResolver
        {
            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var prop = base.CreateProperty(member, memberSerialization);

                if (string.Equals(prop.PropertyName, "senha", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(prop.PropertyName, "password", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(prop.PropertyName, "token", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(prop.PropertyName, "authorization", StringComparison.OrdinalIgnoreCase))
                {
                    prop.ValueProvider = new MaskValueProvider();
                }

                return prop;
            }

            private class MaskValueProvider : IValueProvider
            {
                public object GetValue(object target) => "***";
                public void SetValue(object target, object value) { }
            }
        }

    }
    public sealed class LogEntry
    {
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;
        public string CorrelationId { get; init; } = Guid.NewGuid().ToString();
        public string UserId { get; init; } = string.Empty;
        public string HttpMethod { get; init; } = string.Empty;
        public string Endpoint { get; init; } = string.Empty;
        public BsonDocument Dados { get; init; } = new();
        public string Resultado { get; init; } = string.Empty;
    }
}
