using System.IO;
using CsvLoader.Data.Factories.Interfaces;
using Newtonsoft.Json;

namespace CsvLoader.Data.Factories.Implementations
{
    public class JsonTextWriterFactory : IJsonTextWriterFactory
    {
        public JsonTextWriter CreateInstance(string fileName)
        {
            var textWriter = File.OpenWrite(fileName);

            var streamWriter = new StreamWriter(textWriter);
            return new JsonTextWriter(streamWriter);
        }
    }
}