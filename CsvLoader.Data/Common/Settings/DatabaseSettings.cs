using CsvLoader.Data.Factories.Interfaces;

namespace CsvLoader.Data.Common.Settings
{
    public class DatabaseSettings
    {
        public RepositoryType DefaultDatabase { get; set; }
        public MongoSettings MongoSettings { get; set; }
        public JsonStorageSettings JsonSettings { get; set; }
    }
}