using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Fgc.Infrastructure.Compartilhado.Data.Contexts
{
    public class MongoLogContext
    {
        private readonly IMongoCollection<BsonDocument> _collection;

        public MongoLogContext(IOptions<MongoSettings> mongoSettings)
        {
            var settings = mongoSettings.Value;

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<BsonDocument>(settings.CollectionName);
        }
        public IMongoCollection<BsonDocument> Collection => _collection;

    }
}
