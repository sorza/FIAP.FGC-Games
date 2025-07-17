
using Fgc.Application.Biblioteca.CasosDeUso.Generos.Atualizar;
using MediatR;

namespace Fgc.Api.Endpoints.Generos
{
    public class AtualizarGeneroEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/", HandleAsync)
            .WithName("Generos: Atualizar")
            .WithSummary("Atualiza um genero")
            .WithDescription("Atualiza um genero");

        private static async Task<IResult> HandleAsync(
            ISender sender,
            Command cmd,
            CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(cmd.id, out var guid))
                return TypedResults.BadRequest(new
                {
                    Code = "404",
                    Message = $"'{cmd.id}' não é um id válido."
                });

            var result = await sender.Send(cmd, cancellationToken);

            IResult response = result.IsFailure
                ? TypedResults.Conflict(new { result.Error.Code, result.Error.Message })
                : TypedResults.Created($"/v1/generos/{result.Value.Id}", result.Value);

            return response;
        }
    }
}
