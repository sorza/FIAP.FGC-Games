using Fgc.Application.Biblioteca.Repositorios;
using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Domain.Biblioteca.Entidades;
using Fgc.Infrastructure.Biblioteca.Repositorios;
using Fgc.Infrastructure.Compartilhado.Repositorios;
using Microsoft.Extensions.DependencyInjection;

namespace Fgc.Infrastructure.Compartilhado
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IRepository<Genero>, EFRepository<Genero>>();

            return services;
        }
    }
}
