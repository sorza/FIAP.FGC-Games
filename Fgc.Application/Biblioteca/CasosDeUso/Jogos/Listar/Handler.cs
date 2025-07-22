using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Results;

namespace Fgc.Application.Biblioteca.CasosDeUso.Jogos.Listar
{
    public class Handler(IJogoRepository repository) : IQueryHandler<Query, Response>
    {
        public Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
        {
            return repository.ObterTodos(cancellationToken)
                .ContinueWith(task =>
                {
                    var jogos = task.Result;
                   
                    var response = new Response(jogos.Select(j => new Buscar.Response(
                        j.Id, 
                        j.Titulo, 
                        j.Preco, 
                        j.AnoLancamento, 
                        j.Desenvolvedora, 
                        j.Generos.Select(g => new Generos.Buscar.Response(g.Id, g.Nome)).ToList())).ToList());
                    return Result.Success(response);
                }, cancellationToken);
        }
    }
}
