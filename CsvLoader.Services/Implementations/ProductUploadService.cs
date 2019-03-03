using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using CsvLoader.Data.Entities;
using CsvLoader.Data.Factories.Interfaces;
using CsvLoader.Data.Repositories.Interfaces;
using CsvLoader.Services.Interfaces;

namespace CsvLoader.Services.Implementations
{
    public class ProductUploadService : IProductUploadService
    {
        private readonly IProductRepositoryFactory _productRepositoryFactory;
        private readonly IFactory _factory;

        public ProductUploadService(IProductRepositoryFactory productRepositoryFactory, IFactory factory)
        {
            _productRepositoryFactory = productRepositoryFactory;
            _factory = factory;
        }

        public async Task UploadCsvAndPersistProductsAsync(StreamReader stream)
        {
            using (var csvReader = _factory.CreateReader(stream))
            {
                //this will yield records, so it means that it only returns a record when you iterate the list
                var products = csvReader.GetRecords<Product>();

                var productRepository = _productRepositoryFactory.CreateInstance();
                await productRepository.InsertManyAsync(products);
            }
        }
    }
}