using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Results;

namespace Fgc.Application.Biblioteca.CasosDeUso.Biblioteca.Remover
{
    public sealed class Handler(IBibliotecaRepository repository) : ICommandHandler<Command, Response>
    {
        public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
        {
            var biblioteca = await repository.ObterPorId(Guid.Parse(request.Id));

            if(biblioteca is null)
                return Result.Failure<Response>(new Error("404", "Biblioteca não encontrada."));

            await repository.Deletar(biblioteca.Id);
            return Result.Success(new Response(biblioteca.Id));

        }
    }
}
