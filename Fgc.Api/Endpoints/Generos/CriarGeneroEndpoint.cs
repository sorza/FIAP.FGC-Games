using Fgc.Api.Endpoints.Abstracoes;
using Fgc.Application.Biblioteca.CasosDeUso.Generos.Criar;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Fgc.Api.Endpoints.Generos
{
    public class CriarGeneroEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Generos: Criar")
            .WithSummary("Cria um novo genero")
            .WithDescription("Cria um novo genero")
            .Produces<Response>();
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
            catch(Application.Compartilhado.Comportamentos.ValidationException ex)
            {
                return TypedResults.BadRequest(ex.Errors);

            }
        }
    }
}
