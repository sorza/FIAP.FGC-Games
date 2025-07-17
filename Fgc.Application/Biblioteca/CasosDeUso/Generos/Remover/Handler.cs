using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Results;

namespace Fgc.Application.Biblioteca.CasosDeUso.Generos.Remover
{
    public sealed class Handler(IGeneroRepository generoRepository) : ICommandHandler<Command, Response>
    {
        public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
        {
            var generoExistente = await generoRepository.VerificaSeGeneroExisteAsync(request.id, cancellationToken);

            if(!generoExistente)
                return Result.Failure<Response>(new Error("400","Este gênero não existe."));

            await generoRepository.Deletar(request.id, cancellationToken);
            
            return Result.Success(new Response(request.id));
        }
    }
}
