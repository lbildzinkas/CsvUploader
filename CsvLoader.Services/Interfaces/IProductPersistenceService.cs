using System.Threading.Tasks;

namespace CsvLoader.Services.Interfaces
{
    public interface IProductPersistenceService
    {
        Task ReadUploadedCsvAndPersistProductsAsync(string filePath, System.Threading.CancellationToken cancellationToken);
    }
}