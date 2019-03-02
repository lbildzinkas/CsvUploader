namespace CsvLoader.Data.Entities
{
    public class Product
    {
        public string Key { get; set; }
        public short ArtikelCode { get; set; }
        public string ColorCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
        public string DeliveredIn { get; set; }
        public string Q1 { get; set; }
        public short Size { get; set; }
        public string Color { get; set; }
    }
}