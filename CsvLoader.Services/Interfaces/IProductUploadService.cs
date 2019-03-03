using System.IO;
using System.Threading.Tasks;

namespace CsvLoader.Services.Interfaces
{
    public interface IProductUploadService
    {
        Task UploadCsvAndPersistProductsAsync(StreamReader stream);
    }
}