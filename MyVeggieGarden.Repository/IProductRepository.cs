using MyVeggieGarden.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyVeggieGarden.Repository
{
    public interface IProductRepository
    {
        public Task<Product> GetAsync(int productId);

        public Task<Product> GetByName(string productName);

        public Task<List<Product>> GetAll();
    }
}
