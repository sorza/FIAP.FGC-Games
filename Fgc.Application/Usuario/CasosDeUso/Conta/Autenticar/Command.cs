using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Usuario.CasosDeUso.Conta.Autenticar
{
    public sealed record Command(string email, string senha) : ICommand<Response>;
}
