using Fgc.Application.Compartilhado.Comportamentos;

namespace Fgc.Application.Compartilhado.Services
{
    public interface ILogService
    {
        Task LogAsync(LogEntry logEntry);
    }
}
