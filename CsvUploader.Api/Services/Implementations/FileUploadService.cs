using System.IO;
using System.Threading.Tasks;
using CsvUploader.Api.Contracts.Commands;
using CsvUploader.Api.Providers.Interfaces;
using CsvUploader.Api.Services.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Net.Http.Headers;

namespace CsvUploader.Api.Services.Implementations
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IBus _bus;
        private readonly IMultipartRequestUtilitiesProvider _multipartRequestUtilitiesProvider;
        private static readonly FormOptions DefaultFormOptions = new FormOptions();
        public FileUploadService(IBus bus, IMultipartRequestUtilitiesProvider multipartRequestUtilitiesProvider)
        {
            _multipartRequestUtilitiesProvider = multipartRequestUtilitiesProvider;
            _bus = bus;
        }

        public async Task UploadFile(string contentType, Stream body)
        {
            var boundary = _multipartRequestUtilitiesProvider.GetBoundary(
                            MediaTypeHeaderValue.Parse(contentType),
                            DefaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = _multipartRequestUtilitiesProvider.CreateMultipartReader(boundary, body);

            var section = await reader.ReadNextSectionAsync();
            while (section != null)
            {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);

                if (hasContentDispositionHeader)
                {
                    if (_multipartRequestUtilitiesProvider.HasFileContentDisposition(contentDisposition))
                    {
                        var targetFilePath = Path.GetTempFileName();
                        using (var targetStream = System.IO.File.Create(targetFilePath))
                        {
                            await section.Body.CopyToAsync(targetStream);
                        }

                        await _bus.Publish<PersistProductDataCommand>(new {
                            FileName = targetFilePath
                        });
                    }
                }

                // Drains any remaining section body that has not been consumed and
                // reads the headers for the next section.
                section = await reader.ReadNextSectionAsync();
            }
        }
    }
}