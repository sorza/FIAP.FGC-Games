using Serilog.Context;

namespace Fgc.Api.Middlewares
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next)
            => _next = next;

        public async Task InvokeAsync(HttpContext ctx)
        {
            var correlationId = ctx.Request.Headers["X-Correlation-ID"]
                                .FirstOrDefault()
                              ?? Guid.NewGuid().ToString();

            ctx.Response.Headers["X-Correlation-ID"] = correlationId;
            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                await _next(ctx);
            }
        }
    }

    public static class CorrelationIdMiddlewareExtensions
    {
        public static IApplicationBuilder UseCorrelationId(
            this IApplicationBuilder app)
            => app.UseMiddleware<CorrelationIdMiddleware>();
    }
}
