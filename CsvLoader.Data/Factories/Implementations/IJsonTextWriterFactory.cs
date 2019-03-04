using Newtonsoft.Json;

namespace CsvLoader.Data.Factories.Implementations
{
    public interface IJsonTextWriterFactory
    {
        JsonTextWriter CreateInstance(string fileName);
    }
}