using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Results;

namespace Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas.Criar
{
    public sealed class Handler(IBibliotecaRepository repository) : ICommandHandler<Command, Response>
    {
        public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
        {
            //Verifica se já existe uma biblioteca para a conta informada
            var result = await repository.VerificaSeBibliotecaExisteAsync(request.ContaId, cancellationToken);

            if(result)            
                return Result.Failure<Response>(new Error("409", "Já existe uma biblioteca cadastrada para esta conta."));


            var biblioteca = Domain.Biblioteca.Entidades.Biblioteca.Criar(request.ContaId, request.Titulo);

            await repository.Cadastrar(biblioteca, cancellationToken);

            return Result.Success(new Response(biblioteca.Id, biblioteca.ContaId,biblioteca.Titulo));
        }
    }
}
