using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Results;

namespace Fgc.Application.Biblioteca.CasosDeUso.Jogos.Criar
{
    public class Handler(IJogoRepository jogoRepository) : ICommandHandler<Command, Response>
    {
        public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
        {
            //Verifica se o jogo já existe
            var result = await jogoRepository.ObterPorId(request.Id)
            // Cria o jogo

            // Salva o jogo no repositório

            // Retorna o resultado com os dados do jogo criado
        }
    }
}
