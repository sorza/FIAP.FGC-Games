using Fgc.Infrastructure.Compartilhado.Data;
using Fgc.Infrastructure.Compartilhado.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Fgc.Infrastructure.Biblioteca.Repositorios
{
    public class BibliotecaRepository : GenericoRepository<Domain.Biblioteca.Entidades.Biblioteca>, Application.Compartilhado.Repositorios.Abstracoes.IBibliotecaRepository
    {
        private readonly AppDbContext _context;
        public BibliotecaRepository(AppDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> VerificaSeBibliotecaExisteAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.Bibliotecas.AsNoTracking().AnyAsync(a => a.ContaId == id, cancellationToken);
    }
}
