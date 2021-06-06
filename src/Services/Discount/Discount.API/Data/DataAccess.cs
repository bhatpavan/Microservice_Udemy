using Dapper;
using Discount.API.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UtilityLibrary;
using System;

namespace Discount.API.Data
{
    public class DataAccess
    {
 
        public DataAccess()
        {
             //connection = new NpgsqlConnection
             //(Configuration._Configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            
        }
        public async Task<IEnumerable<T>> RunQuerry<T>(string querry, object model)
        {
            using var connection = new NpgsqlConnection
             (Configuration._Configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            return await connection.QueryAsync<T>
                (querry,model);

        }

        public async Task<int> ExecuteQuerry(string querry, object model)
        {
            using var connection = new NpgsqlConnection
               (Configuration._Configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            return await connection.ExecuteAsync(querry, model);

        }
    }
}
