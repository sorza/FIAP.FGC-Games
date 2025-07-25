﻿using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Results;
using Fgc.Domain.Biblioteca.Entidades;

namespace Fgc.Application.Biblioteca.CasosDeUso.Generos.Criar
{
    public sealed class Handler(IGeneroRepository generoRepository) : ICommandHandler<Command, Response>
    {
        public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
        {
            
            var generoExistente = await generoRepository.VerificaSeGeneroExisteAsync(request.Genero, cancellationToken);

            if(generoExistente)
                return Result.Failure<Response>(new Error("400","Este gênero já está cadastrado."));

            var genero = Genero.Criar(request.Genero);

            await generoRepository.Cadastrar(genero);
            
            return Result.Success(new Response(genero.Id, genero.Nome));
        }
    }
}
