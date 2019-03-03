using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvLoader.Data.Common.Settings;
using CsvLoader.Data.Entities;
using CsvLoader.Data.Factories.Implementations;
using CsvLoader.Data.Factories.Interfaces;
using CsvLoader.Data.Repositories.Interfaces;
using MongoDB.Driver;

namespace CsvLoader.Data.Repositories.Implementations
{
    public class MongoProductRepository : IProductRepository
    {
        private readonly IMongoDatabase _db;
        private readonly string ProductCollectionName = "Products";
        private Settings _mongoSettings;

        public MongoProductRepository(Settings mongoSettings, IMongoDatabaseFactory mongoDatabaseFactory)
        {
            _db = mongoDatabaseFactory.CreateInstance(mongoSettings.ConnectionString, mongoSettings.DatabaseName);
            _mongoSettings = mongoSettings;
        }

        public async Task InsertManyAsync(IEnumerable<Product> products)
        {
            var productsCollection = _db.GetCollection<Product>(ProductCollectionName);

            //This is only enumerated later one, mongo driver will handle the batching, this way we should not have any memory leaks
            var productsToInsert = products.Select(p => new InsertOneModel<Product>(p));
            await productsCollection.BulkWriteAsync(productsToInsert, new BulkWriteOptions() { IsOrdered = false });
        }
    }
}