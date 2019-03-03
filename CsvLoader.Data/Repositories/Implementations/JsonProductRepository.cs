using System.Collections.Generic;
using System.Threading.Tasks;
using CsvLoader.Data.Common.Settings;
using CsvLoader.Data.Entities;
using CsvLoader.Data.Factories.Implementations;
using CsvLoader.Data.Repositories.Interfaces;

namespace CsvLoader.Data.Repositories.Implementations
{
    public class JsonProductRepository : IProductRepository
    {
        private Settings _jsonSettings;

        public JsonProductRepository(Settings jsonSettings)
        {
            _jsonSettings = jsonSettings;
        }

        public Task InsertManyAsync(IEnumerable<Product> products)
        {
            throw new System.NotImplementedException();
        }
    }
}