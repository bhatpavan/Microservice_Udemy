using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Product_Catalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product_Catalog.Data
{
    public class CatalogContext : ICatalogContext
    {
        public  CatalogContext(IConfiguration configuration)
        {
            
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            //Seed the Value
            if (!Products.Find(x => true).Any())
            {
                CatalogContextSeed.SeedData(Products);
            }
        }
        public IMongoCollection<Product> Products { get; }
    }
}
