using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CsvLoader.Data.Common.Settings;
using CsvLoader.Data.Entities;
using CsvLoader.Data.Factories.Implementations;
using CsvLoader.Data.Factories.Interfaces;
using CsvLoader.Data.Repositories.Interfaces;
using Newtonsoft.Json;

namespace CsvLoader.Data.Repositories.Implementations
{
    public class JsonProductRepository : IProductRepository
    {
        private readonly JsonStorageSettings _jsonSettings;
        private readonly IJsonTextWriterFactory _jsonTextWriterFactory;

        public JsonProductRepository(JsonStorageSettings jsonSettings, IJsonTextWriterFactory jsonTextWriterFactory)
        {
            _jsonSettings = jsonSettings;
            _jsonTextWriterFactory = jsonTextWriterFactory;
        }

        public async Task InsertManyAsync(IEnumerable<Product> products, CancellationToken cancellationToken)
        {
            var productProperties = typeof(Product).GetProperties();
            var fileName = $"{_jsonSettings.FilePath}{Guid.NewGuid()}.json";
            using (var writer = _jsonTextWriterFactory.CreateInstance(fileName))
            {
                writer.Formatting = Formatting.Indented;
                await writer.WriteStartArrayAsync(cancellationToken);
                {
                    foreach (var product in products)
                    {
                        await writer.WriteStartObjectAsync(cancellationToken);
                        {
                            foreach (var productProperty in productProperties)
                            {
                                await writer.WritePropertyNameAsync(productProperty.Name, cancellationToken);
                                await writer.WriteValueAsync(productProperty.GetValue(product), cancellationToken);
                            }
                        }
                        await writer.WriteEndObjectAsync(cancellationToken);
                    }
                }
                await writer.WriteEndArrayAsync(cancellationToken);
            }
        }
    }
}