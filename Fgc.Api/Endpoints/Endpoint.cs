using Fgc.Api.Endpoints.Abstracoes;
using Fgc.Api.Endpoints.Generos;
using Fgc.Api.Endpoints.Jogos;

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

            endpoints.MapGroup("v1/jogos")
                .WithTags("Jogos")
                .MapEndpoint<CriarJogoEndpoint>()
                .MapEndpoint<BuscarJogoEndpoint>()
                .MapEndpoint<ListarJogosEndpoint>();

        }
        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
            where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
