using Fgc.Application.Compartilhado.Comportamentos;
using Fgc.Application.Compartilhado.Services;
using Fgc.Infrastructure.Compartilhado.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Fgc.Infrastructure.Compartilhado.Services
{
    public class MongoLogService : ILogService
    {
        private readonly IMongoCollection<LogEntry> _collection;

        public MongoLogService(IMongoDatabase database, IOptions<MongoSettings> settings)
        {
            _collection = database.GetCollection<LogEntry>(settings.Value.CollectionName);
        }

        public async Task LogAsync(LogEntry logEntry)
        {
            await _collection.InsertOneAsync(logEntry);
        }

    }
}
