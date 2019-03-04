using System.IO;
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