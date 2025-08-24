using Fgc.Api.Common.Api;
using Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas.AdicionarJogo;
using Fgc.Application.Compartilhado.Comportamentos;
using MediatR;

namespace Fgc.Api.Endpoints.Biblioteca
{
    public class AdicionarJogoBibliotecaEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/adicionarJogo", HandleAsync)
            .WithName("Biblioteca: Adicionar Jogo")
            .WithSummary("Adiciona um jogo a biblioteca")
            .WithDescription("Adiciona um jogo a biblioteca")
            .Produces<Response>(StatusCodes.Status201Created)
            .Produces<Response>(StatusCodes.Status400BadRequest)
            .Produces<Response>(StatusCodes.Status409Conflict)
            .Produces<Response>(StatusCodes.Status404NotFound)
            .RequireAuthorization();

        private static async Task<IResult> HandleAsync(
            Command cmd,
            ISender sender,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await sender.Send(cmd, cancellationToken);
                return result.IsFailure
                    ? result.Error.Code switch
                    {
                        "404" => TypedResults.NotFound(new { result.Error, result.Error.Message }),
                        "409" => TypedResults.Conflict(new { result.Error, result.Error.Message }),
                        _ => TypedResults.BadRequest(new { result.Error, result.Error.Message })
                    }
                    : TypedResults.Created($"/AdicionarJogo/{result.Value.Id}", result.Value);
            }
            catch (ValidationException ex)
            {
                return TypedResults.BadRequest(new { ex.Message, ex.Errors });
            }
        }
    }
}
