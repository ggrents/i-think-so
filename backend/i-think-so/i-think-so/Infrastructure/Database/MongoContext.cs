using i_think_so.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace i_think_so.Infrastructure.Database
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }
        public IMongoCollection<Survey> Surveys=> _database.GetCollection<Survey>("surveys");
    }
}
