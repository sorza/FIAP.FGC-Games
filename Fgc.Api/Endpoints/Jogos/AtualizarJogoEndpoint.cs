﻿using Fgc.Api.Endpoints.Abstracoes;
using Fgc.Application.Biblioteca.CasosDeUso.Jogos.Atualizar;
using MediatR;

namespace Fgc.Api.Endpoints.Jogos
{
    public class AtualizarJogoEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/", HandleAsync)
            .WithName("Jogos: Atualizar")
            .WithSummary("Atualiza um jogo")
            .WithDescription("Atualiza um jogo");

        private static async Task<IResult> HandleAsync(
            ISender sender,
            Command cmd,
            CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(cmd.Id, out var guid))
                return TypedResults.BadRequest(new
                {
                    Code = "404",
                    Message = $"'{cmd.Id}' não é um id válido."
                });
            var result = await sender.Send(cmd, cancellationToken);
            IResult response = result.IsFailure
                ? TypedResults.Conflict(new { result.Error.Code, result.Error.Message })
                : TypedResults.Created($"/v1/jogos/{result.Value.Id}", result.Value);
            return response;
        }
    }
}
