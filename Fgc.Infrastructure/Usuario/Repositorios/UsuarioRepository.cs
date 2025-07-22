using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Domain.Usuario.Entidades;
using Fgc.Infrastructure.Compartilhado.Data;
using Fgc.Infrastructure.Compartilhado.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Fgc.Infrastructure.Usuario.Repositorios
{
    public class UsuarioRepository : GenericoRepository<Conta>, IUsuarioRepository
    {
        private readonly AppDbContext _context;
        public UsuarioRepository(AppDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool VerificaSeUsuarioExiste(string email, CancellationToken cancellationToken = default)
        {
            return _context.Contas
                .AsNoTracking()
                .Any(u => u.Email.Address == email);
        }
    }
}
