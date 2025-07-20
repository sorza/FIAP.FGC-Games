using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Results;
using Fgc.Domain.Biblioteca.Entidades;

namespace Fgc.Application.Biblioteca.CasosDeUso.Generos.Buscar
{
    public class Handler(IGeneroRepository repository) : IQueryHandler<Query, Response>
    {
        public async Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken = default)
        {
            var genero = await repository.ObterPorId(Guid.Parse(request.Id), cancellationToken);

            if (genero is null)
                return Result.Failure<Response>(new Error("404", "Este gênero não existe."));

            return Result.Success(new Response(genero.Id, genero.Nome));
        }
    }
}
