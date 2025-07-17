using Fgc.Api.Endpoints.Abstracoes;
using Fgc.Application.Biblioteca.CasosDeUso.Jogos.Criar;
using MediatR;

namespace Fgc.Api.Endpoints.Jogos
{
    public class CriarJogoEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Jogos: Criar")
            .WithSummary("Cria um novo jogo")
            .WithDescription("Cria um novo jogo")
            .Produces<Response>();

        private static async Task<IResult> HandleAsync(
            ISender sender,
            Command cmd,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(cmd, cancellationToken);
            IResult response = result.IsFailure
                ? TypedResults.Conflict(new { result.Error.Code, result.Error.Message })
                : TypedResults.Created($"/{result}", result.Value);
            return response;
        }
    }
}
