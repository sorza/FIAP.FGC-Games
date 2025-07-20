using Fgc.Api.Endpoints.Abstracoes;
using Fgc.Application.Biblioteca.CasosDeUso.Jogos.Remover;
using MediatR;

namespace Fgc.Api.Endpoints.Jogos
{
    public class RemoverJogoEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapDelete("/{id}", HandleAsync)
            .WithName("Jogos: Remover")
            .WithSummary("Remove um jogo")
            .WithDescription("Remove um jogo")
            .Produces(StatusCodes.Status204NoContent)
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
            var result = await sender.Send(new Command(guid), cancellationToken);
            IResult response = result.IsFailure
                ? TypedResults.NotFound(new { result.Error.Code, result.Error.Message })
                : TypedResults.NoContent();
            return response;
        }
    }
}
