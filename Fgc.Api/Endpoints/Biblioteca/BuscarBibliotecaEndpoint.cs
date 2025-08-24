using Fgc.Api.Common.Api;
using Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas.Buscar;
using Fgc.Application.Compartilhado.Comportamentos;
using MediatR;

namespace Fgc.Api.Endpoints.Biblioteca
{
    public class BuscarBibliotecaEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
             => app.MapGet("/{id}", HandleAsync)
                 .WithName("Bibliotecas: Buscar")
                 .WithSummary("Busca uma biblioteca por id")
                 .WithDescription("Busca uma biblioteca por id")
                 .Produces<Response>(StatusCodes.Status200OK)
                 .Produces<Response>(StatusCodes.Status404NotFound);
        private static async Task<IResult> HandleAsync(
            ISender sender,
            string id,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await sender.Send(new Query(id), cancellationToken);
                return result.IsFailure
                    ? TypedResults.NotFound(new { result.Error.Code, result.Error.Message })
                    : TypedResults.Ok(result.Value);
            }
            catch (ValidationException ex)
            {
                return TypedResults.BadRequest(new { ex.Message, ex.Errors });
            }
        }
    }
}
