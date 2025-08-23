namespace Fgc.Api.CustomMiddlewares
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("X-Correlation-Id", out var correlationId))
                correlationId = Guid.NewGuid().ToString();

            context.Items["CorrelationId"] = correlationId;
            context.Response.Headers["X-Correlation-Id"] = correlationId;

            await _next(context);
        }
    }
}
