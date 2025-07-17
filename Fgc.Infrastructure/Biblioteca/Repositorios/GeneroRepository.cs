using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Domain.Biblioteca.Entidades;
using Fgc.Infrastructure.Compartilhado.Data;
using Fgc.Infrastructure.Compartilhado.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Fgc.Infrastructure.Biblioteca.Repositorios
{
    public class GeneroRepository : GenericoRepository<Genero>, IGeneroRepository
    {
        private readonly AppDbContext _context;
        public GeneroRepository(AppDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> VerificaSeGeneroExisteAsync(string genero, CancellationToken cancellationToken = default)
           => await _context.Generos.AsNoTracking().AnyAsync(a => a.Nome == genero);

        public async Task<bool> VerificaSeGeneroExisteAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.Generos.AsNoTracking().AnyAsync(a => a.Id == id);
    }
}
