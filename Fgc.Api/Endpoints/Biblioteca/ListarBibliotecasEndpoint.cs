using Fgc.Api.Common.Api;
using Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas.Listar;
using Fgc.Application.Compartilhado.Comportamentos;
using MediatR;

namespace Fgc.Api.Endpoints.Biblioteca
{
    public class ListarBibliotecasEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Bibliotecas: Listar")
            .WithSummary("Lista todas as bibliotecas")
            .WithDescription("Lista todas as bibliotecas")
            .Produces<Response>(StatusCodes.Status400BadRequest)
            .Produces<Response>(StatusCodes.Status200OK);

        private static async Task<IResult> HandleAsync(
            ISender sender,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await sender.Send(new Query(), cancellationToken);
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
