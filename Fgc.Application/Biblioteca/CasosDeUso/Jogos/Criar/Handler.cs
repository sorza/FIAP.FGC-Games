using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Results;
using Fgc.Domain.Biblioteca.Entidades;

namespace Fgc.Application.Biblioteca.CasosDeUso.Jogos.Criar
{
    public class Handler(IJogoRepository jogoRepository) : ICommandHandler<Command, Response>
    {
        public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
        {                                              
            var jogo = Jogo.Criar(
                request.Titulo, 
                request.Preco, 
                request.AnoLancamento, 
                request.Desenvolvedora, 
                request.Generos.Select(g => Genero.Criar(g.Id, g.Genero)).ToList());

            var jogoExistente = await jogoRepository.VerificaSeJogoExisteAsync(jogo);

            if (jogoExistente)
                return Result.Failure<Response>(new Error("409", "Este jogo já está cadastrado."));                       
           
            await jogoRepository.Cadastrar(jogo, cancellationToken);
            
            return Result.Success(new Response(jogo.Id, jogo.Titulo, jogo.Preco, jogo.AnoLancamento, jogo.Desenvolvedora, request.Generos));
        }
    }
}
