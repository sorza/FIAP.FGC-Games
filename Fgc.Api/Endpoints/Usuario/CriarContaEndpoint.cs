using Fgc.Api.Endpoints.Abstracoes;
using Fgc.Application.Compartilhado.Comportamentos;
using Fgc.Application.Usuario.CasosDeUso.Conta.Criar;
using MediatR;

namespace Fgc.Api.Endpoints.Usuario
{
    public class CriarContaEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Usuario: Criar Conta")
            .WithSummary("Cria uma nova conta de usuário")
            .WithDescription("Cria uma nova conta de usuário com nome, email e senha")
            .Produces<Response>(StatusCodes.Status201Created)
            .Produces<Response>(StatusCodes.Status409Conflict)
            .Produces<Response>(StatusCodes.Status400BadRequest);

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
                    : TypedResults.Created($"/{result}", result.Value);
                return response;
            }
            catch (ValidationException ex)
            {
                return TypedResults.BadRequest(new { ex.Message, ex.Errors });
            }
        }
    }
}
