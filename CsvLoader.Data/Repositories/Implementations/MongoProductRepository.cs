using System.Collections.Generic;
using System.Threading.Tasks;
using CsvLoader.Data.Entities;
using CsvLoader.Data.Factories.Implementations;
using CsvLoader.Data.Repositories.Interfaces;

namespace CsvLoader.Data.Repositories.Implementations
{
    public class MongoProductRepository : IProductRepository
    {
        private MongoSettings _mongoSettings;

        public MongoProductRepository(MongoSettings mongoSettings)
        {
            _mongoSettings = mongoSettings;
        }

        public Task InsertManyAsync(IEnumerable<Product> products)
        {
            throw new System.NotImplementedException();
        }
    }
}