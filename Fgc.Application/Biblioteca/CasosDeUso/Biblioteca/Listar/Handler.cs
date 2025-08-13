using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Results;

namespace Fgc.Application.Biblioteca.CasosDeUso.Biblioteca.Listar
{
    public sealed class Handler(IBibliotecaRepository repository) : IQueryHandler<Query, Response>
    {
        public async Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
        {
            var bibliotecas = await repository.ObterTodos(cancellationToken);

            return Result.Success(new Response(bibliotecas.Select(b => new Buscar.Response(b.Id, b.ContaId, b.Titulo )).ToList()));
        }
    }
}
