using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Results;

namespace Fgc.Application.Biblioteca.CasosDeUso.Biblioteca.Buscar
{
    public class Handler(IBibliotecaRepository repository) : IQueryHandler<Query, Response>
    {
        public async Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
        {
            var biblioteca = await repository.ObterPorId(Guid.Parse(request.Id), cancellationToken);

            if(biblioteca is null)
                return Result.Failure<Response>(new Error("404", "Biblioteca não encontrada."));

            return Result.Success(new Response(biblioteca.Id, biblioteca.ContaId, biblioteca.Titulo));
        }
    }
}
