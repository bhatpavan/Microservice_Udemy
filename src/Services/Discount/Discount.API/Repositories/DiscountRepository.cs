using Discount.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using UtilityLibrary;
using Microsoft.Extensions.Configuration;
using Dapper;
using Discount.API.Data;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        DataAccess database;
        public DiscountRepository()
        {
            database = new DataAccess();
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            var affected = await database.ExecuteQuerry
                ("insert into Coupon (ProductName, Description, Amount) values (@pro,@Des,@amo)",
                new {  Des = coupon.Description, pro = coupon.ProductName, Amo = coupon.Amount });
            if (affected == 0)
                return false;
            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            var affected = await database.ExecuteQuerry
                ("delete from Coupon where productname =@productName",
                new { productName = productName });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            var coupon = (await database.RunQuerry<Coupon>("select * from Coupon where ProductName=@productname",
                new { ProductName = productName })).ToList();
            
            if (coupon.Count<1)
            {
                return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Applied" };
            }
           // database.Dispose();
            return coupon.First();
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            var affected = (await database.RunQuerry<int>
                 ("update Coupon Set ProductName=@ProductName, Description=@Description,Amount=@Amount where Id= @Id",
                 coupon)).ToList();

            if (affected.First() == 0)
                return false;

            return true;
        }
    }
}
