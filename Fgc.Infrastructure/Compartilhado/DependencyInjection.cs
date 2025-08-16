using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Services;
using Fgc.Infrastructure.Biblioteca.Repositorios;
using Fgc.Infrastructure.Compartilhado.Data;
using Fgc.Infrastructure.Compartilhado.Services;
using Fgc.Infrastructure.Usuario.Repositorios;
using Microsoft.Extensions.DependencyInjection;

namespace Fgc.Infrastructure.Compartilhado
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IGeneroRepository, GeneroRepository>();
            services.AddScoped<IJogoRepository, JogoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IBibliotecaRepository, BibliotecaRepository>();
            services.AddScoped<IBibliotecaJogoRepository, BibliotecaJogoRepository>();
            services.AddSingleton<IJwtTokenService, JwtTokenService>();

            return services;
        }
    }
}
