using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Results;

namespace Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas.AdicionarJogo
{
    public sealed class Handler(IBibliotecaRepository bibliotecaRepository, IJogoRepository jogoRepository) : ICommandHandler<Command, Response>
    {
        public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
        {
            var biblioteca = await bibliotecaRepository.ObterPorId(Guid.Parse(request.BibliotecaId), cancellationToken);
            if (biblioteca is null)            
                return Result.Failure<Response>(new Error("404","Biblioteca não encontrada."));

            var jogo = await jogoRepository.ObterPorId(Guid.Parse(request.JogoId), cancellationToken);
            if (jogo is null)            
                return Result.Failure<Response>(new Error("404","Jogo não encontrado."));           

            if(biblioteca.Jogos.Any(j => j.JogoId == jogo.Id))
                return Result.Failure<Response>(new Error("409", "Este jogo já está adicionado na biblioteca."));

            biblioteca.AdicionarJogo(jogo);
            await bibliotecaRepository.SaveAsync(cancellationToken);

            return Result.Success(new Response(
                biblioteca.Id,
                biblioteca.Id,
                jogo.Id,
                DateTime.UtcNow,
                jogo.Preco
            ));

        }
    }
}
