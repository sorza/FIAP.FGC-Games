using Fgc.Application.Biblioteca.CasosDeUso.Generos.Listar;
using MediatR;

namespace Fgc.Api.Endpoints.Generos
{
    public class ListarGenerosEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Generos: Listar")
            .WithSummary("Lista todos os generos")
            .WithDescription("Lista todos os generos");

        private static async Task<IResult> HandleAsync(
            ISender sender,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(new Query(), cancellationToken);
            IResult response = result.IsFailure
                ? TypedResults.NotFound(new { result.Error.Code, result.Error.Message })
                : TypedResults.Ok(result.Value);
            return response;
        }
    }
}
