using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using MyVeggieGarden.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Linq;

namespace MyVeggieGarden.Repository
{
    public class ProductRepository : IProductRepository
    {
        IConfiguration _config;

        public ProductRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<Product>> GetAll()
        {
            IEnumerable<Product> products;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                products = await connection.QueryAsync<Product>("Product_GetAll",
                                                                new { }, 
                                                                commandType: CommandType.StoredProcedure);
            }

            return products.ToList();
        }

        public async Task<Product> GetAsync(int productId)
        {
            Product product;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                product = await connection.QueryFirstOrDefaultAsync<Product>("Product_GetById",
                                                                              new { ProductId = productId },
                                                                              commandType: CommandType.StoredProcedure);
            }

            return product;
        }

        public async Task<Product> GetByName(string productName)
        {
            Product product;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                product = await connection.QueryFirstOrDefaultAsync<Product>("Product_GetByName",
                                                                              new { ProductName = productName },
                                                                              commandType: CommandType.StoredProcedure);
            }

            return product;
        }
    }
}
