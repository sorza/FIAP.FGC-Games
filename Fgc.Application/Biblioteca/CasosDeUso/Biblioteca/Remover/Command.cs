using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using System.Windows.Input;

namespace Fgc.Application.Biblioteca.CasosDeUso.Biblioteca.Remover
{
    public sealed record Command(string Id) : ICommand<Response>;  
}
