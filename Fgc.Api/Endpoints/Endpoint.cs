using Fgc.Api.Endpoints.Abstracoes;
using Fgc.Api.Endpoints.Biblioteca;
using Fgc.Api.Endpoints.Generos;
using Fgc.Api.Endpoints.Jogos;
using Fgc.Api.Endpoints.Usuario;

namespace Fgc.Api.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("");                      

            endpoints.MapGroup("v1/generos")
                .WithTags("Gêneros")
                .MapEndpoint<CriarGeneroEndpoint>()
                .MapEndpoint<BuscarGeneroEndpoint>()
                .MapEndpoint<ListarGenerosEndpoint>()
                .MapEndpoint<AtualizarGeneroEndpoint>()
                .MapEndpoint<RemoverGeneroEndpoint>();

            endpoints.MapGroup("v1/jogos")
                .WithTags("Jogos")
                .MapEndpoint<CriarJogoEndpoint>()
                .MapEndpoint<BuscarJogoEndpoint>()
                .MapEndpoint<ListarJogosEndpoint>()
                .MapEndpoint<AtualizarJogoEndpoint>()
                .MapEndpoint<RemoverJogoEndpoint>();              

            endpoints.MapGroup("v1/usuarios")
                .WithTags("Usuários")
                .MapEndpoint<CriarContaEndpoint>()
                .MapEndpoint<AutenticarEndpoint>();

            endpoints.MapGroup("v1/biblioteca")
                .WithTags("Bibliotecas")
                .MapEndpoint<CriarBibliotecaEndpoint>()
                .MapEndpoint<BuscarBibliotecaEndpoint>()
                .MapEndpoint<ListarBibliotecasEndpoint>()
                .MapEndpoint<AtualizarBibliotecaEndpoint>()
                .MapEndpoint<RemoverBibliotecaEndpoint>()
                .MapEndpoint<AdicionarJogoBibliotecaEndpoint>()
                .MapEndpoint<RemoverJogoBibliotecaEndpoint>();

        }
        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
            where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
