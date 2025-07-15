using Fgc.Domain.Biblioteca.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Fgc.Infrastructure.Compartilhado.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Jogo> Jogos { get; set; } = null!;
        public DbSet<Genero> Generos { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);
        }
    }
}
