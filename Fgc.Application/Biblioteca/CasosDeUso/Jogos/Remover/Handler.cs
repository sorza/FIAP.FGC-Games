using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Results;

namespace Fgc.Application.Biblioteca.CasosDeUso.Jogos.Remover
{
    public sealed class Handler(IJogoRepository jogoRepository, IBibliotecaJogoRepository bjRepository) : ICommandHandler<Command, Response>
    {
        public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
        {
            var jogo = await jogoRepository.ObterPorId(Guid.Parse(request.Id), cancellationToken);

            var jogoVinculado = await bjRepository.JogoEstaEmAlgumaBiblioteca(Guid.Parse(request.Id), cancellationToken);
           
            if (jogoVinculado)
                return Result.Failure<Response>(new Error("409", "Não é possivel remover jogos que estão vinculados a uma biblioteca."));

            if (jogo is null)
                return Result.Failure<Response>(new Error("404", "Jogo não encontrado."));

            await jogoRepository.Deletar(Guid.Parse(request.Id), cancellationToken);
            return Result.Success(new Response(Guid.Parse(request.Id)));
        }
    }
}
