using Fgc.Api.Endpoints.Abstracoes;
using Fgc.Application.Biblioteca.CasosDeUso.Generos.Buscar;
using Fgc.Application.Compartilhado.Comportamentos;
using MediatR;

namespace Fgc.Api.Endpoints.Generos
{
    public class BuscarGeneroEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{id}", HandleAsync)
                .WithName("Generos: Buscar")
                .WithSummary("Busca um genero por id")
                .WithDescription("Busca um genero por id")
                .Produces<Response>(StatusCodes.Status200OK)
                .Produces<Response>(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest);
        private static async Task<IResult> HandleAsync(
            ISender sender,
            string id,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await sender.Send(new Query(id), cancellationToken);
                IResult response = result.IsFailure
                    ? TypedResults.NotFound(new { result.Error.Code, result.Error.Message })
                    : TypedResults.Ok(result.Value);
                return response;
            }
            catch(ValidationException ex)
            {
                return TypedResults.BadRequest(new { ex.Message, ex.Errors });
            }
        }
    }
}
