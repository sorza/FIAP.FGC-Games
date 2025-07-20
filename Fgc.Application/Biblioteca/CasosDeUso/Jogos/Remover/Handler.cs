using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Results;

namespace Fgc.Application.Biblioteca.CasosDeUso.Jogos.Remover
{
    public sealed class Handler(IJogoRepository repository) : ICommandHandler<Command, Response>
    {
        public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
        {
            await repository.Deletar(Guid.Parse(request.Id), cancellationToken);
            return Result.Success(new Response(Guid.Parse(request.Id)));
        }
    }
}
