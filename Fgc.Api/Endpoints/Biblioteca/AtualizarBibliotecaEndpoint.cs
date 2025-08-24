using Fgc.Api.Common.Api;
using Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas.Atualizar;
using Fgc.Application.Compartilhado.Comportamentos;
using MediatR;

namespace Fgc.Api.Endpoints.Biblioteca
{
    public class AtualizarBibliotecaEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("", HandleAsync)
            .WithName("Bibliotecas: Atualizar")
            .WithSummary("Atualiza uma biblioteca")
            .WithDescription("Atualiza uma biblioteca")
            .Produces<Response>(StatusCodes.Status200OK)
            .Produces<Response>(StatusCodes.Status400BadRequest)
            .Produces<Response>(StatusCodes.Status409Conflict)
            .RequireAuthorization();

        private static async Task<IResult> HandleAsync(
            ISender sender,
            Command cmd,
            CancellationToken cancellationToken)
        {
            try
            {               
                var result = await sender.Send(cmd, cancellationToken);
                return result.IsFailure
                    ? TypedResults.Conflict(new { result.Error.Code, result.Error.Message })
                    : TypedResults.Ok(result.Value);
            }
            catch (ValidationException ex)
            {
                return TypedResults.BadRequest(new { ex.Message, ex.Errors });
            }
        }
    }
}
