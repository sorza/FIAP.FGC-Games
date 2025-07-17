using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Domain.Biblioteca.Entidades;
using Fgc.Infrastructure.Compartilhado.Data;
using Microsoft.EntityFrameworkCore;

namespace Fgc.Infrastructure.Compartilhado.Repositorios
{
    public class GeneroRepository : EFRepository<Genero>, IGeneroRepository
    {
        private readonly AppDbContext _context;
        public GeneroRepository(AppDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> VerificaSeGeneroExisteAsync(string genero, CancellationToken cancellationToken = default)
           => await _context.Generos.AsNoTracking().AnyAsync(a => a.Nome == genero);

    }
}
