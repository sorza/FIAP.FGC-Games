using Fgc.Api.Common.Api;
using Fgc.Application.Biblioteca.CasosDeUso.Jogos.Atualizar;
using Fgc.Application.Compartilhado.Comportamentos;
using MediatR;

namespace Fgc.Api.Endpoints.Jogos
{
    public class AtualizarJogoEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/", HandleAsync)
            .WithName("Jogos: Atualizar")
            .WithSummary("Atualiza um jogo")
            .WithDescription("Atualiza um jogo")
            .Produces<Response>(StatusCodes.Status200OK)
            .Produces<Response>(StatusCodes.Status409Conflict)
            .Produces<Response>(StatusCodes.Status400BadRequest)
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
            catch(ValidationException ex)
            {
                return TypedResults.BadRequest(new { ex.Message, ex.Errors });
            }
        }
    }
}
