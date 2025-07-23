
using Fgc.Api.Endpoints.Abstracoes;
using Fgc.Application.Biblioteca.CasosDeUso.Generos.Atualizar;
using Fgc.Application.Compartilhado.Comportamentos;
using MediatR;

namespace Fgc.Api.Endpoints.Generos
{
    public class AtualizarGeneroEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/", HandleAsync)
            .WithName("Generos: Atualizar")
            .WithSummary("Atualiza um genero")
            .WithDescription("Atualiza um genero")
            .Produces<Response>(StatusCodes.Status200OK)
            .Produces<Response>(StatusCodes.Status400BadRequest)
            .Produces<Response>(StatusCodes.Status409Conflict)
            .RequireAuthorization("SomenteAdmin");

        private static async Task<IResult> HandleAsync(
            ISender sender,
            Command cmd,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await sender.Send(cmd, cancellationToken);

                IResult response = result.IsFailure
                    ? TypedResults.Conflict(new { result.Error.Code, result.Error.Message })
                    : TypedResults.Ok(result.Value);

                return response;
            }
            catch (ValidationException ex)
            {
                return TypedResults.BadRequest(new { ex.Message, ex.Errors });
            }            
        }
    }
}
