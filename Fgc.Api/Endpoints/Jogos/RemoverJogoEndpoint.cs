using Fgc.Api.Endpoints.Abstracoes;
using Fgc.Application.Biblioteca.CasosDeUso.Jogos.Remover;
using Fgc.Application.Compartilhado.Comportamentos;
using Fgc.Application.Compartilhado.Results;
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
            .Produces<Response>(StatusCodes.Status204NoContent)
            .Produces<Response>(StatusCodes.Status404NotFound)
            .Produces<Response>(StatusCodes.Status409Conflict)
            .Produces<Response>(StatusCodes.Status400BadRequest)
            .RequireAuthorization("SomenteAdmin");

        private static async Task<IResult> HandleAsync(
            ISender sender,
            string id,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await sender.Send(new Command(id), cancellationToken);

                if (result.IsFailure)
                {
                    return result.Error.Code switch
                    {
                        "404" => TypedResults.NotFound(new Error ("404", result.Error.Message)),
                        "409" => TypedResults.Conflict(new Error("409", result.Error.Message)),
                        _ => TypedResults.BadRequest(new Error("400", result.Error.Message))
                    };
                }

                return TypedResults.NoContent();
            }
            catch (ValidationException ex)
            {
                return TypedResults.BadRequest(new { ex.Message, ex.Errors });

            }
        }
    }
}
