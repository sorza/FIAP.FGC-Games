using Fgc.Api.Endpoints.Generos;

namespace Fgc.Api.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("");

            endpoints.MapGet("/", () => "Fgc.Api está no ar!")
                .WithName("Página Inicial")
                .WithSummary("Home Endpoint")
                .WithDescription("Este é o endpoint da Home da Fgc.Api");

            endpoints.MapGroup("v1/generos")
                .WithTags("Gêneros")
                .MapEndpoint<CriarGeneroEndpoint>()
                .MapEndpoint<BuscarGeneroEndpoint>()
                .MapEndpoint<ListarGenerosEndpoint>()
                .MapEndpoint<AtualizarGeneroEndpoint>()
                .MapEndpoint<RemoverGeneroEndpoint>();

        }
        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
            where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
