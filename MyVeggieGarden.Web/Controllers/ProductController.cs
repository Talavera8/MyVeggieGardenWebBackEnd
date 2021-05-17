using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVeggieGarden.Models.Product;
using MyVeggieGarden.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVeggieGarden.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var products = await _productRepository.GetAll();

            return products;
        } 

        [HttpGet("GetProductById/{productId}")]
        public async Task<ActionResult<Product>> GetProductById(int productId)
        {
            var product = await _productRepository.GetAsync(productId);

            return product;
        }

        [HttpGet("GetProductByName")]
        public async Task<ActionResult<Product>> GetProductByName(string productName)
        {
            var product = await _productRepository.GetByName(productName);

            return product;
        }
    }
}
