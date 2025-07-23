using Fgc.Api.Endpoints.Abstracoes;
using Fgc.Application.Biblioteca.CasosDeUso.Jogos.Remover;
using Fgc.Application.Compartilhado.Comportamentos;
using MediatR;

namespace Fgc.Api.Endpoints.Jogos
{
    public class RemoverJogoEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapDelete("/{id}", HandleAsync)
            .WithName("Jogos: Remover")
            .WithSummary("Remove um jogo")
            .WithDescription("Remove um jogo")
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
                IResult response = result.IsFailure
                    ? TypedResults.NotFound(new { result.Error.Code, result.Error.Message })
                    : TypedResults.NoContent();
                return response;
            }
            catch (ValidationException ex)
            {
                return TypedResults.BadRequest(new { ex.Message, ex.Errors });

            }
        }
    }
}
