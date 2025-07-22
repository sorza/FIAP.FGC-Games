using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Domain.Biblioteca.Entidades;
using Fgc.Domain.Usuario.Entidades;
using Fgc.Infrastructure.Compartilhado.Data;
using Fgc.Infrastructure.Compartilhado.Repositorios;

namespace Fgc.Infrastructure.Usuario.Repositorios
{
    public class UsuarioRepository : GenericoRepository<Conta>, IUsuarioRepository
    {
        private readonly AppDbContext _context;
        public UsuarioRepository(AppDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
