using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specifications;
using API.DTO;
using System.Linq;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productsRepo; 
        private readonly IGenericRepository<ProductBrand> _brandsRepo; 
        private readonly IGenericRepository<ProductType> _typesRepo;

        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> brandsRepo, IGenericRepository<ProductType> typesRepo, IMapper mapper)
        {
            this._productsRepo = productsRepo;
            this._brandsRepo = brandsRepo;
            this._typesRepo = typesRepo;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await this._productsRepo.GetListAsyncWithSpec(spec);

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product =  await this._productsRepo.GetEntityWithSpec(spec);
            return _mapper.Map<Product, ProductToReturnDto>(product);

        }
    
        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetBrands(){
            return Ok(await this._brandsRepo.GetListAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetTypes(){
            return Ok(await this._typesRepo.GetListAsync());
        }
    }
}