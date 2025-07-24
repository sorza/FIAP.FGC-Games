using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Results;
using Fgc.Application.Compartilhado.Services;
using Fgc.Domain.Usuario.ObjetosDeValor;

namespace Fgc.Application.Usuario.CasosDeUso.Conta.Autenticar
{
    public class Handler(IUsuarioRepository repository, IJwtTokenService jwtService) : ICommandHandler<Command, Response>
    {
        public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
        {
            var email = Email.Criar(request.email);

            var conta = await repository.Autenticar(email, cancellationToken);

            if (conta is null)            
                return Result.Failure<Response>(new Error("401", "Email ou senha inválidos."));

            if (!conta.Senha.Verificar(request.senha))
                return Result.Failure<Response>(new Error("401", "Email ou senha inválidos."));

            if (!conta.Ativo)
                return Result.Failure<Response>(new Error("403", "Conta inativa."));
           
            var tokenInfo = jwtService.GerarToken(conta);

            var response = new Response(
            tokenInfo.Token,
            tokenInfo.Validade);

            return Result.Success(response);
        }

    }
}
