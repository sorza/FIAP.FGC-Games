using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Services;
using Fgc.Infrastructure.Biblioteca.Repositorios;
using Fgc.Infrastructure.Compartilhado.Data;
using Fgc.Infrastructure.Compartilhado.Data.Contexts;
using Fgc.Infrastructure.Compartilhado.Services;
using Fgc.Infrastructure.Usuario.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Fgc.Infrastructure.Compartilhado
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<MongoLogContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IGeneroRepository, GeneroRepository>();
            services.AddScoped<IJogoRepository, JogoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IBibliotecaRepository, BibliotecaRepository>();
            services.AddScoped<IBibliotecaJogoRepository, BibliotecaJogoRepository>();
            services.AddSingleton<IJwtTokenService, JwtTokenService>();
            services.AddSingleton<ILogService, MongoLogService>();

            // Registra IMongoClient e IMongoDatabase usando as configs
            services.AddSingleton<IMongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
                return new MongoClient(settings.ConnectionString);
            });

            services.AddSingleton<IMongoDatabase>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(settings.DatabaseName);
            });

            services.AddSingleton<ILogService, MongoLogService>();

            return services;
        }
    }
}
