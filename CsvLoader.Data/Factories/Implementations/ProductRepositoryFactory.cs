using System.Collections.Generic;
using System.ComponentModel.Design;
using CsvLoader.Data.Common.Settings;
using CsvLoader.Data.Factories.Interfaces;
using CsvLoader.Data.Repositories.Implementations;
using CsvLoader.Data.Repositories.Interfaces;
using Microsoft.Extensions.Options;

namespace CsvLoader.Data.Factories.Implementations
{
    public class ProductRepositoryFactory : IProductRepositoryFactory
    {
        private readonly Dictionary<RepositoryType, IProductRepository> _dicInstances;
        private readonly IOptionsMonitor<DatabaseSettings> _databaseSettings;

        public ProductRepositoryFactory(IOptionsMonitor<DatabaseSettings> databaseSettings, IMongoDatabaseFactory mongoDatabaseFactory)
        {
            _dicInstances = new Dictionary<RepositoryType, IProductRepository>()
            {
                {RepositoryType.Json, new JsonProductRepository(databaseSettings.CurrentValue.JsonSettings)},
                { RepositoryType.MongoDb, new MongoProductRepository(databaseSettings.CurrentValue.MongoSettings, mongoDatabaseFactory) }
            };
            _databaseSettings = databaseSettings;
        }

        public IProductRepository CreateInstance()
        {
            return _dicInstances[_databaseSettings.CurrentValue.DefaultDatabase];
        }

        public IEnumerable<IProductRepository> CreateInstances()
        {
            return _dicInstances.Values;
        }
    }
}