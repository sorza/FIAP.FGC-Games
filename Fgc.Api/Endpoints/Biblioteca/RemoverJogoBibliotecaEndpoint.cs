using Fgc.Api.Endpoints.Abstracoes;
using Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas.RemoverJogo;
using Fgc.Application.Compartilhado.Comportamentos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fgc.Api.Endpoints.Biblioteca
{
    public class RemoverJogoBibliotecaEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/removerJogo", HandleAsync)
            .WithName("Biblioteca: Remover Jogo")
            .WithSummary("Remove um jogo da biblioteca")
            .WithDescription("Remove um jogo da biblioteca")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<Response>(StatusCodes.Status404NotFound)
            .Produces<Response>(StatusCodes.Status400BadRequest);

        private static async Task<IResult> HandleAsync(
           [FromBody]Command cmd,
           [FromServices]ISender sender,
            CancellationToken cancellationToken)
        {
            try
            {                
                var result = await sender.Send(cmd, cancellationToken);
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
