using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Results;

namespace Fgc.Application.Biblioteca.CasosDeUso.Jogos.Buscar
{
    public class Handler(IJogoRepository jogoRepository) : IQueryHandler<Query, Response>
    {
        public async Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
        {
            var jogo = await jogoRepository.ObterPorId(request.id, cancellationToken);

            if (jogo is null)
                return Result.Failure<Response>(new Error("404", $"Jogo não encontrado."));

            return Result.Success(new Response(jogo.Id, jogo.Titulo, jogo.Preco, jogo.DataLancamento, jogo.Desenvolvedora, jogo.Generos.ToList()));

        }
    }
}
