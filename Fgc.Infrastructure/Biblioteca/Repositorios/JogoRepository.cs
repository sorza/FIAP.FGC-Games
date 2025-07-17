using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Domain.Biblioteca.Entidades;
using Fgc.Infrastructure.Compartilhado.Data;
using Fgc.Infrastructure.Compartilhado.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Fgc.Infrastructure.Biblioteca.Repositorios
{
    public class JogoRepository : GenericoRepository<Jogo>, IJogoRepository
    {
        private readonly AppDbContext _context;
        public JogoRepository(AppDbContext context) : base(context)
        {
           _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> VerificaSeJogoExisteAsync(Jogo jogo, CancellationToken cancellationToken = default)
               => await _context.Jogos.AsNoTracking().AnyAsync(x => x == jogo);
    }
}
