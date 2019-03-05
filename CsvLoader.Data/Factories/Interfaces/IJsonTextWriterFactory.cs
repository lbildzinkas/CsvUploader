using Newtonsoft.Json;

namespace CsvLoader.Data.Factories.Interfaces
{
    public interface IJsonTextWriterFactory
    {
        JsonTextWriter CreateInstance(string fileName);
    }
}