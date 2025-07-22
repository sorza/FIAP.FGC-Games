using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using Fgc.Domain.Usuario.Enums;
using Fgc.Domain.Usuario.ObjetosDeValor;

namespace Fgc.Application.Usuario.CasosDeUso.Conta.Criar
{
    public sealed record Response(Guid Id, string Nome, Senha Senha, Email Email, ETipoPerfil TipoPerfil, bool Ativo) : ICommandResponse;   
}
