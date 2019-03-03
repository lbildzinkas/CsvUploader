using CsvLoader.Data.Factories.Interfaces;
using MongoDB.Driver;

namespace CsvLoader.Data.Factories.Implementations
{
    public class MongoDatabaseFactory : IMongoDatabaseFactory
    {
        public IMongoDatabase CreateInstance(string connectionString, string dbName)
        {
            var dbClient = new MongoClient(connectionString);
            return dbClient.GetDatabase(dbName);
        }
    }
}