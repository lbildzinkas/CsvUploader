using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using CsvLoader.Data.Entities;
using CsvLoader.Data.Factories.Interfaces;
using CsvLoader.Services.Interfaces;

namespace CsvLoader.Services.Implementations
{
    public class ProductPersistenceService : IProductPersistenceService
    {
        private readonly IProductRepositoryFactory _productRepositoryFactory;
        private readonly IFactory _factory;

        public ProductPersistenceService(IProductRepositoryFactory productRepositoryFactory, IFactory factory)
        {
            _productRepositoryFactory = productRepositoryFactory;
            _factory = factory;
        }

        public async Task ReadUploadedCsvAndPersistProductsAsync(string filePath, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using(var stream = File.OpenText(filePath))
            using (var csvReader = _factory.CreateReader(stream, new CsvHelper.Configuration.Configuration(){
                Delimiter = ",",
                HasHeaderRecord = true,
                HeaderValidated = null,
                MissingFieldFound = null,
                IgnoreBlankLines = false,
                ReadingExceptionOccurred = null
            }))
            {
                //this will yield records, so it means that it only returns a record when you iterate the list
                var products = csvReader.GetRecords<Product>();

                var productRepository = _productRepositoryFactory.CreateInstance();
                await productRepository.InsertManyAsync(products, cancellationToken);
            }
        }
    }
}