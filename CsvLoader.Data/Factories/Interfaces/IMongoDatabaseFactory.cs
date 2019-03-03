using MongoDB.Driver;

namespace CsvLoader.Data.Factories.Interfaces
{
    public interface IMongoDatabaseFactory
    {
        IMongoDatabase CreateInstance(string connectionString, string dbName);
    }
}