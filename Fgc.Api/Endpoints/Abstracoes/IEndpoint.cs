namespace Fgc.Api.Endpoints.Abstracoes
{
    public interface IEndpoint
    {
        static abstract void Map(IEndpointRouteBuilder app);
    }
}
