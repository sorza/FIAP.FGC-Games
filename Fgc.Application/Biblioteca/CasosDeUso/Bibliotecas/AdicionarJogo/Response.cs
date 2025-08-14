using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas.AdicionarJogo
{
    public sealed record Response(Guid Id, Guid BibliotecaId, Guid JogoId, DateTime DataAquisicao, decimal ValorPago) : ICommandResponse;   
}
