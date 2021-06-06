using MongoDB.Driver;
using Product_Catalog.Entities;

namespace Product_Catalog.Data
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }

    }
} 
