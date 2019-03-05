using System.IO;
using System.Threading.Tasks;

namespace CsvUploader.Api.Services.Interfaces
{
    public interface IFileUploadService
    {
        Task UploadFile(string contentType, Stream body);
    }
}