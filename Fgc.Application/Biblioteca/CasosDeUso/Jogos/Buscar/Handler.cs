using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Results;

namespace Fgc.Application.Biblioteca.CasosDeUso.Jogos.Buscar
{
    public class Handler(IJogoRepository jogoRepository) : IQueryHandler<Query, Response>
    {
        public async Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
        {
            var jogo = await jogoRepository.ObterPorId(Guid.Parse(request.Id), cancellationToken);

            if (jogo is null)
                return Result.Failure<Response>(new Error("404", $"Jogo não encontrado."));

            return Result.Success(new Response(
                jogo.Id, 
                jogo.Titulo, 
                jogo.Preco, 
                jogo.DataLancamento, 
                jogo.Desenvolvedora, 
                jogo.Generos.Select(g => new Generos.Buscar.Response(g.Id, g.Nome)).ToList()));
        }
    }
}
