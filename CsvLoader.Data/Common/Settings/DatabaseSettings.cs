using CsvLoader.Data.Factories.Interfaces;

namespace CsvLoader.Data.Common.Settings
{
    public class DatabaseSettings
    {
        public RepositoryType DefaultDatabase { get; set; }
        public Settings MongoSettings { get; set; }
        public Settings JsonSettings { get; set; }
    }
}