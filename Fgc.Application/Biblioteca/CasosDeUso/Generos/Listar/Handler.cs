using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Results;

namespace Fgc.Application.Biblioteca.CasosDeUso.Generos.Listar
{
    public class Handler(IGeneroRepository repository) : IQueryHandler<Query, Response>
    {  
        public async Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
        {
            var generos = await repository.ObterTodos(cancellationToken);           
           
            return Result.Success(new Response(generos.Select(g => new Buscar.Response(g.Id, g.Nome)).ToList()));
        }
    }
}
