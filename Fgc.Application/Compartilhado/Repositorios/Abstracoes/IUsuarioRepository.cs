using Fgc.Domain.Usuario.Entidades;
using Fgc.Application.Usuario.CasosDeUso.Conta.Autenticar;
using Fgc.Domain.Usuario.ObjetosDeValor;

namespace Fgc.Application.Compartilhado.Repositorios.Abstracoes
{
    public interface IUsuarioRepository : IRepository<Conta>
    {
        Task<Conta?> Autenticar(Email email, CancellationToken cancellationToken);
        public bool VerificaSeUsuarioExiste(string email, CancellationToken cancellationToken = default);
    }
}
