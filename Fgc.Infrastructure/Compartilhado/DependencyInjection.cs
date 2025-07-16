using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Infrastructure.Compartilhado.Repositorios;
using Microsoft.Extensions.DependencyInjection;

namespace Fgc.Infrastructure.Compartilhado
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Repositorios
            services.AddScoped<IGeneroRepository, GeneroRepository>();

            return services;
        }
    }
}
