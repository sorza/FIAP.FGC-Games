﻿using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Results;
using Fgc.Domain.Biblioteca.Entidades;
using MediatR;

namespace Fgc.Application.Biblioteca.CasosDeUso.Jogos.Atualizar
{
    public sealed class Handler(IJogoRepository repository) : ICommandHandler<Command, Response>
    {
        async Task<Result<Response>> IRequestHandler<Command, Result<Response>>.Handle(Command request, CancellationToken cancellationToken)
        {
            var jogo = await repository.ObterPorId(Guid.Parse(request.Id), cancellationToken);

            if (jogo is null)
                throw new KeyNotFoundException("Jogo não encontrado.");

            var generos = request.Generos
                .Select(g => Genero.Criar(g.Id, g.Genero))
                .ToList();

            jogo.Atualizar(request.Titulo, request.Preco, request.DataLancamento, request.Desenvolvedora, generos);

            await repository.Alterar(jogo, cancellationToken);

            return Result.Success(new Response
            (
                jogo.Id,
                jogo.Titulo,
                jogo.Preco,
                jogo.DataLancamento,
                jogo.Desenvolvedora,
                request.Generos
            ));
        }
    }
}
