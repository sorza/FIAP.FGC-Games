using Fgc.Api.Endpoints.Abstracoes;
using Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas.Remover;
using Fgc.Application.Compartilhado.Comportamentos;
using MediatR;

namespace Fgc.Api.Endpoints.Biblioteca
{
    public class RemoverBibliotecaEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
           .WithName("Bibliotecas: Remover")
           .WithSummary("Remove uma biblioteca")
           .WithDescription("Remove uma biblioteca")
           .Produces(StatusCodes.Status204NoContent)
           .Produces<Response>(StatusCodes.Status404NotFound)
           .Produces(StatusCodes.Status400BadRequest)
           .RequireAuthorization("SomenteAdmin");

        private static async Task<IResult> HandleAsync(
            ISender sender,
            string id,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await sender.Send(new Command(id), cancellationToken);
                return result.IsFailure
                    ? TypedResults.NotFound(new { result.Error.Code, result.Error.Message })
                    : TypedResults.NoContent();
            }
            catch (ValidationException ex)
            {
                return TypedResults.BadRequest(new { ex.Message, ex.Errors });
            }

        }
    }
}
