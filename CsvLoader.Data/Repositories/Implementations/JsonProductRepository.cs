using System.Collections.Generic;
using System.Threading.Tasks;
using CsvLoader.Data.Entities;
using CsvLoader.Data.Factories.Implementations;
using CsvLoader.Data.Repositories.Interfaces;

namespace CsvLoader.Data.Repositories.Implementations
{
    public class JsonProductRepository : IProductRepository
    {
        private JsonSettings _jsonSettings;

        public JsonProductRepository(JsonSettings jsonSettings)
        {
            _jsonSettings = jsonSettings;
        }

        public Task InsertManyAsync(IEnumerable<Product> products)
        {
            throw new System.NotImplementedException();
        }
    }
}