using Fgc.Api.Common.Api;
using Fgc.Application.Compartilhado.Comportamentos;
using Fgc.Application.Compartilhado.Services;
using Fgc.Application.Usuario.CasosDeUso.Conta.Autenticar;
using Fgc.Domain.Usuario.ObjetosDeValor;
using MediatR;

namespace Fgc.Api.Endpoints.Usuario
{
    public class AutenticarEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/login/", HandleAsync)
            .WithName("Usuario: Autenticar")
            .WithSummary("Autentica um usuário")
            .WithDescription("Realiza a autenticação de um usuário com email e senha")
            .Produces<TokenInfo>(StatusCodes.Status200OK)
            .Produces<Response>(StatusCodes.Status401Unauthorized)
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
                    ? TypedResults.Unauthorized()
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
