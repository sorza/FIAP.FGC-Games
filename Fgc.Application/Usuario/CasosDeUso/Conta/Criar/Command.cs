using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Domain.Usuario.Enums;
using Fgc.Domain.Usuario.ObjetosDeValor;

namespace Fgc.Application.Usuario.CasosDeUso.Conta.Criar
{
    public sealed record Command(string nome, string senha, string email, ETipoPerfil tipoPerfil) : ICommand<Response>;
}
