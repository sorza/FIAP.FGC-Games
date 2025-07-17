using Fgc.Api.Endpoints.Abstracoes;
using Fgc.Application.Biblioteca.CasosDeUso.Generos.Remover;
using MediatR;

namespace Fgc.Api.Endpoints.Generos
{
    public class RemoverGeneroEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Generos: Remover")
            .WithSummary("Remove um genero")
            .WithDescription("Remove um genero");
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
