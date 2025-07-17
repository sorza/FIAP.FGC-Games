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
            if (string.IsNullOrWhiteSpace(request.titulo) || 
                request.preco <= 0 ||  
                string.IsNullOrWhiteSpace(request.desenvolvedora) /*||  
                request.generos == null || !request.generos.Any()*/)            
                return Result.Failure<Response>(new Error("400", "Dados inválidos para criação do jogo."));   
                       
            var jogo = Jogo.Criar(
                request.titulo,
                request.preco,
                request.dataLancamento,
                request.desenvolvedora,
                request.generos);

            var jogoExistente = await jogoRepository.VerificaSeJogoExisteAsync(jogo);

            if (jogoExistente)
                return Result.Failure<Response>(new Error("400", "Este jogo já está cadastrado."));                       
           
            await jogoRepository.Cadastrar(jogo, cancellationToken);
            
            return Result.Success(new Response(jogo.Id, jogo.Titulo, jogo.Preco, jogo.DataLancamento, jogo.Desenvolvedora, jogo.Generos.ToList()));
        }
    }
}
