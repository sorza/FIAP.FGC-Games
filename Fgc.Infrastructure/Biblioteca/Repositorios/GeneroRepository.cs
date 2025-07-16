using Fgc.Domain.Biblioteca.Entidades;
using Fgc.Infrastructure.Compartilhado.Data;
using Fgc.Infrastructure.Compartilhado.Repositorios;

namespace Fgc.Infrastructure.Biblioteca.Repositorios
{
    public class GeneroRepository : EFRepository<Genero>
    {
        public GeneroRepository(AppDbContext context) : base(context)
        {
        }
    }
}
