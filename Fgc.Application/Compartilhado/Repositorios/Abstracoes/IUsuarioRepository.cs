using Fgc.Domain.Usuario.Entidades;
using Fgc.Domain.Usuario.ObjetosDeValor;

namespace Fgc.Application.Compartilhado.Repositorios.Abstracoes
{
    public interface IUsuarioRepository : IRepository<Conta>
    {
        public bool VerificaSeUsuarioExiste(string email, CancellationToken cancellationToken = default);
    }
}
