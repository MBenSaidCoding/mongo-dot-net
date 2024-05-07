
using Microsoft.AspNetCore.Mvc;
using Microsoft.Docs.Samples;
using WashingtonStoreWebApi.Infrastructure.Products;
using WashingtonStoreWebApi.Models;

namespace WashingtonStoreWebApi.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class ProductsController:ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string searchKeyWord)
        {
           var products = await _productRepository.Search(searchKeyWord);

           return Ok(products);
        } 

        [HttpGet("{id}")]
    
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var result = await _productRepository.GetById(id);
            if(result is not {})
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            await _productRepository.InsertOne(product);
            return CreatedAtAction(nameof(GetByIdAsync), new {id=product.ProductId}, product);
        }
    }
}