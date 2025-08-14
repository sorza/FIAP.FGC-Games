using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;

namespace Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas.AdicionarJogo
{
    public sealed record Command(string BibliotecaId, string JogoId): ICommand<Response>;     
}
