using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Usuario.CasosDeUso.Conta.Criar
{
    public sealed record Command(string nome, string senha, string email) : ICommand<Response>;
}
