using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Results;

namespace Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas.RemoverJogo
{
    public sealed class Handler(IBibliotecaRepository bibliotecaRepository) : ICommandHandler<Command, Response>
    {
        public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
        {
            var biblioteca = await bibliotecaRepository.ObterPorId(Guid.Parse(request.BibliotecaId), cancellationToken);
            if (biblioteca is null)
                return Result.Failure<Response>(new Error("404", "Biblioteca não encontrada."));

            var jogo = biblioteca.Jogos.FirstOrDefault(j => j.JogoId == Guid.Parse(request.JogoId));
            if (jogo is null)
                return Result.Failure<Response>(new Error("404", "Jogo não encontrado na biblioteca."));

            biblioteca.RemoverJogo(jogo.JogoId);
            await bibliotecaRepository.SaveAsync(cancellationToken);

            return Result.Success(new Response(
                jogo.BibliotecaId,
                jogo.JogoId
            ));
        }
    }
}
