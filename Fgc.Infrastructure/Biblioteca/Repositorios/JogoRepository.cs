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

        public override async Task Cadastrar(Jogo jogo, CancellationToken cancellationToken = default)
        {
            foreach (var genero in jogo.Generos.ToList())
            {
                var generoStub = Genero.Criar(genero.Id, genero.Nome);

                _context.Entry(generoStub).State = EntityState.Unchanged;

                jogo.RemoverGenero(genero);
                jogo.AdicionarGenero(generoStub);
            }
            await base.Cadastrar(jogo, cancellationToken);
        }

        public override async Task<Jogo?> ObterPorId(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Jogos
                .Include(j => j.Generos)
                .AsNoTracking()
                .FirstOrDefaultAsync(j => j.Id == id, cancellationToken);
        }

        public override async Task<IList<Jogo>> ObterTodos(CancellationToken cancellationToken = default)
        {
            return await _context.Jogos
                .Include(j => j.Generos)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
