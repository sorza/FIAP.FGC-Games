using Serilog;

namespace Fgc.Api.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
            => _next = next;

        public async Task InvokeAsync(HttpContext ctx)
        {
            var start = DateTime.UtcNow;
            await _next(ctx);
            var elapsed = DateTime.UtcNow - start;

            Log.Information(
                "HTTP {Method} {Path} retornou {StatusCode} em {Elapsed:0.0000} ms",
                ctx.Request.Method,
                ctx.Request.Path,
                ctx.Response.StatusCode,
                elapsed.TotalMilliseconds);
        }
    }

    public static class RequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(
            this IApplicationBuilder app)
            => app.UseMiddleware<RequestLoggingMiddleware>();
    }
}
