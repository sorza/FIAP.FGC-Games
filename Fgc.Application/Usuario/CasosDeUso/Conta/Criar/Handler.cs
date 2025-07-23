using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Results;
using Fgc.Domain.Usuario.Enums;

namespace Fgc.Application.Usuario.CasosDeUso.Conta.Criar
{
    public class Handler(IUsuarioRepository repository) : ICommandHandler<Command, Response>
    {
        public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
        {
           var usuarioExistente = repository.VerificaSeUsuarioExiste(request.email, cancellationToken);

           if (usuarioExistente)
                return Result.Failure<Response>(new Error("409", "Este usuário já está cadastrado."));

            var usuario = Domain.Usuario.Entidades.Conta.Criar(
                request.nome,
                request.senha,
                request.email,
                ETipoPerfil.Comum);

            await repository.Cadastrar(usuario, cancellationToken);

            return Result.Success(new Response(
                usuario.Id,
                usuario.Nome,
                usuario.Senha,
                usuario.Email,
                usuario.Perfil,
                usuario.Ativo));
        }
    }
}
