using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Results;

namespace Fgc.Application.Biblioteca.CasosDeUso.Generos.Atualizar
{
    public sealed class Handler(IGeneroRepository generoRepository) : ICommandHandler<Command, Response>
    {
        public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
        {
            var genero = await generoRepository.VerificaSeGeneroExisteAsync(request.id, cancellationToken);

            if(!genero)
                return Result.Failure<Response>(new Error("400", "Este gênero não existe."));

            var generoExistente = await generoRepository.ObterPorId(Guid.Parse(request.id), cancellationToken);
            generoExistente!.Atualizar(request.nome);

            await generoRepository.Alterar(generoExistente);

            return Result.Success(new Response(generoExistente.Id, generoExistente.Nome));

        }
    }
}
