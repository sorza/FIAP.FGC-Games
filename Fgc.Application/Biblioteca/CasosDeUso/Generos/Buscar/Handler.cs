using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Results;

namespace Fgc.Application.Biblioteca.CasosDeUso.Generos.Buscar
{
    public class Handler(IGeneroRepository repository) : IQueryHandler<Query, Response>
    {
        public async Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken = default)
        {
            var genero = await repository.ObterPorId(request.Id, cancellationToken);

            if (genero is null)
                return Result.Failure<Response>(new Error("404", $"Gênero com id {request.Id} não encontrado."));

            return Result.Success(new Response(genero.Id, genero.Nome));
        }
    }
}
