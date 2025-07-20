using Fgc.Api.Endpoints.Abstracoes;
using Fgc.Application.Biblioteca.CasosDeUso.Jogos.Buscar;
using MediatR;

namespace Fgc.Api.Endpoints.Jogos
{
    public class BuscarJogoEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Jogos: Buscar")
            .WithSummary("Busca um jogo por id")
            .WithDescription("Busca um jogo por id")
            .Produces<Response>(StatusCodes.Status200OK)
            .Produces<Response>(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest);

        private static async Task<IResult> HandleAsync(
           ISender sender,
           string id,
           CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(id, out var guid))
                return TypedResults.BadRequest(new
                {
                    Code = "404",
                    Message = $"'{id}' não é um id válido."
                });
            var result = await sender.Send(new Query(guid), cancellationToken);
            IResult response = result.IsFailure
                ? TypedResults.NotFound(new { result.Error.Code, result.Error.Message })
                : TypedResults.Ok(result.Value);
            return response;
        }
    }
}
