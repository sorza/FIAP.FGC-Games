using Fgc.Api.Endpoints.Abstracoes;
using Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas.Criar;
using FluentValidation;
using MediatR;

namespace Fgc.Api.Endpoints.Biblioteca
{
    public class CriarBibliotecaEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Biblioteca: Criar")
            .WithSummary("Cria uma nova biblioteca")
            .WithDescription("Cria uma nova biblioteca")
            .Produces<Domain.Biblioteca.Entidades.Biblioteca>(StatusCodes.Status201Created)
            .Produces<Domain.Biblioteca.Entidades.Biblioteca>(StatusCodes.Status409Conflict)
            .Produces<Domain.Biblioteca.Entidades.Biblioteca>(StatusCodes.Status400BadRequest)
            .RequireAuthorization();

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
                    : TypedResults.Created($"/biblioteca/{result.Value.Id}", result.Value);
                return response;
            }
            catch (ValidationException ex)
            {
                return TypedResults.BadRequest(new { ex.Message, ex.Errors });
            }
        }
    }
}
