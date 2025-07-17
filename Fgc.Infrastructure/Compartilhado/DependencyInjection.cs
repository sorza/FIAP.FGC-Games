using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Infrastructure.Biblioteca.Repositorios;
using Microsoft.Extensions.DependencyInjection;

namespace Fgc.Infrastructure.Compartilhado
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IGeneroRepository, GeneroRepository>();
            services.AddScoped<IJogoRepository, JogoRepository>();

            return services;
        }
    }
}
