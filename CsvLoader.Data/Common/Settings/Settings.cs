namespace CsvLoader.Data.Common.Settings
{
    public class Settings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; internal set; }
        public int InsertBatchSize { get; internal set; }
    }
}