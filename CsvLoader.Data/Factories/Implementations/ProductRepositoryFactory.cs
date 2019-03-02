using System.Collections.Generic;
using System.ComponentModel.Design;
using CsvLoader.Data.Factories.Interfaces;
using CsvLoader.Data.Repositories.Implementations;
using CsvLoader.Data.Repositories.Interfaces;
using Microsoft.Extensions.Options;

namespace CsvLoader.Data.Factories.Implementations
{
    public class ProductRepositoryFactory : IProductRepositoryFactory
    {
        private readonly Dictionary<RepositoryType, IProductRepository> _dicInstances;

        public ProductRepositoryFactory(IOptions<DatabaseSettings> databaseSettings)
        {
            _dicInstances = new Dictionary<RepositoryType, IProductRepository>()
            {
                {RepositoryType.Json, new JsonProductRepository(databaseSettings.Value.JsonSettings)},
                { RepositoryType.MongoDb, new MongoProductRepository(databaseSettings.Value.MongoSettings) }
            };
        }

        public IProductRepository CreateInstance(RepositoryType repositoryType)
        {
            return _dicInstances[repositoryType];
        }

        public IEnumerable<IProductRepository> CreateInstances()
        {
            return _dicInstances.Values;
        }
    }
}