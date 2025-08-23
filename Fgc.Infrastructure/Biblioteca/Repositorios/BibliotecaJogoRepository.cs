using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Domain.Biblioteca.Entidades;
using Fgc.Infrastructure.Compartilhado.Data.Contexts;
using Fgc.Infrastructure.Compartilhado.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Fgc.Infrastructure.Biblioteca.Repositorios
{
    public class BibliotecaJogoRepository : GenericoRepository<BibliotecaJogo>, IBibliotecaJogoRepository
    {
        private readonly AppDbContext _context;
        public BibliotecaJogoRepository(AppDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public async Task<bool> JogoEstaEmAlgumaBiblioteca(Guid jogoId, CancellationToken cancellationToken = default)
            => await _context.BibliotecaJogos.AnyAsync(bj => bj.JogoId == jogoId);         
    }
}
