using System.Collections.Generic;
using System.Threading.Tasks;
using CsvLoader.Data.Entities;

namespace CsvLoader.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task InsertManyAsync(IEnumerable<Product> products);
    }
}