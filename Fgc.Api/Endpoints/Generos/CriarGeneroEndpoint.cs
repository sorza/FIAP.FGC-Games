using Fgc.Application.Biblioteca.CasosDeUso.Generos.Criar;
using MediatR;

namespace Fgc.Api.Endpoints.Generos
{
    public class CriarGeneroEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Generos: Criar")
            .WithSummary("Cria um novo genero")
            .WithDescription("Cria um novo genero")
            .Produces<Response>();

        private static async Task<IResult> HandleAsync(
            ISender sender,
            Command cmd,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(cmd, cancellationToken);

            IResult response = result.IsFailure
                ? TypedResults.Conflict(new { result.Error.Code, result.Error.Message })
                : TypedResults.Created($"/v1/generos/{result.Value.Id}", result.Value);

            return response;
        }
    }
}
