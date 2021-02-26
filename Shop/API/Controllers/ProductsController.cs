using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await this._repo.GetProductsAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await this._repo.GetProductByIdAsync(id);

            return Ok(product);
        }
    
        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetBrands(){
            return Ok(await this._repo.GetProductBrandsAnsyc());
        }
        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetTypes(){
            return Ok(await this._repo.GetProductTypesAnsyc());
        }
    }
}