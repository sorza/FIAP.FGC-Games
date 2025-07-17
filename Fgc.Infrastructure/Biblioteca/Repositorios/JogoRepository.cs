using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Domain.Biblioteca.Entidades;
using Fgc.Infrastructure.Compartilhado.Data;
using Fgc.Infrastructure.Compartilhado.Repositorios;

namespace Fgc.Infrastructure.Biblioteca.Repositorios
{
    public class JogoRepository : GenericoRepository<Jogo>, IJogoRepository
    {
        public JogoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
