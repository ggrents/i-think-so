using MongoDB.Driver;

namespace i_think_so.Infrastructure.Database
{
    public class MongoContext
    {
        public MongoContext()
        {
            var client = new MongoClient();
            client.GetDatabase("i-think-so_db");
        }
    }
}
