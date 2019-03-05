using System.Threading.Tasks;
using CsvLoader.Services.Interfaces;
using CsvUploader.Api.Contracts.Commands;
using MassTransit;

namespace CsvUploader.Api.Consumers
{
    /// <summary>
    /// I am consumming the command in the same hosted service just to demonstrate, ideally that should be hosted in another services
    /// to scale your application better
    /// </summary>
    public class PersistProductDataCommandConsumer : IConsumer<PersistProductDataCommand>
    {
        private readonly IProductPersistenceService _productUploadService;
        public PersistProductDataCommandConsumer(IProductPersistenceService productUploadService)
        {
            _productUploadService = productUploadService;

        }

        public async Task Consume(ConsumeContext<PersistProductDataCommand> context)
        {
            var msg = context.Message;

            await _productUploadService.ReadUploadedCsvAndPersistProductsAsync(msg.FileName, context.CancellationToken);
        }
    }
}