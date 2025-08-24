using Fgc.Api.CustomMiddlewares;

namespace Fgc.Api.Common.Api
{
    public static class AppExtensions
    {
        public static void ConfigureDevEnvironment(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fgc.Api v1");
                c.RoutePrefix = string.Empty;
            });
        }

        public static void UseSecurity(this WebApplication app)
        {
            app.UseMiddleware<CorrelationIdMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
