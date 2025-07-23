using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Usuario.CasosDeUso.Conta.Autenticar
{
    public sealed record Response(string TokenDeAcesso, DateTime Validade) : ICommandResponse;
}
