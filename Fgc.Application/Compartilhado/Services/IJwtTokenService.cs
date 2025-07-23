using Fgc.Domain.Usuario.Entidades;

namespace Fgc.Application.Compartilhado.Services
{
    public interface IJwtTokenService
    {
        TokenInfo GenerateToken(Conta user);
    }
    public record TokenInfo(string Token, DateTime ExpiresAt);
}
