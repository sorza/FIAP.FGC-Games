using Fgc.Api.Common.Api;
using Fgc.Application.Biblioteca.CasosDeUso.Jogos.Listar;
using MediatR;

namespace Fgc.Api.Endpoints.Jogos
{
    public class ListarJogosEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Jogos: Listar")
            .WithSummary("Lista todos os jogos")
            .WithDescription("Lista todos os jogos")
            .Produces<Response>(StatusCodes.Status200OK);

        private static async Task<IResult> HandleAsync(
            ISender sender,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(new Query(), cancellationToken);
            IResult response = result.IsFailure
                ? TypedResults.NotFound(new { result.Error.Code, result.Error.Message })
                : TypedResults.Ok(result.Value);
            return response;
        }
    }
}
