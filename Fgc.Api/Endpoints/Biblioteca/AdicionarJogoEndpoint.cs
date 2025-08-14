using Fgc.Api.Endpoints.Abstracoes;
using Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas.AdicionarJogo;
using Fgc.Application.Compartilhado.Comportamentos;
using MediatR;

namespace Fgc.Api.Endpoints.Biblioteca
{
    public class AdicionarJogoEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/adicionarJogo", HandleAsync)
            .WithName("AdicionarJogo")
            .Produces<Response>(StatusCodes.Status201Created)
            .Produces<Response>(StatusCodes.Status400BadRequest)
            .Produces<Response>(StatusCodes.Status404NotFound);

        private static async Task<IResult> HandleAsync(
            Command cmd,
            ISender sender,
            CancellationToken cancellationToken)
        {
            try
            {

                var result = await sender.Send(cmd, cancellationToken);
                return result.IsFailure
                    ? TypedResults.Conflict(new { result.Error.Code, result.Error.Message })
                    : TypedResults.Created($"/AdicionarJogo/{result.Value.Id}", result.Value);
            }
            catch (ValidationException ex)
            {
                return TypedResults.BadRequest(new { ex.Message, ex.Errors });
            }
        }
    }
}
