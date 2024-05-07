
using Microsoft.AspNetCore.Mvc;
using Microsoft.Docs.Samples;
using WashingtonStoreWebApi.Infrastructure.Products;
using WashingtonStoreWebApi.Models;
using Microsoft.AspNetCore.JsonPatch;

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

        [HttpPut]
        public async Task<ActionResult> Replace([FromBody] Product product)
        {
            if (product.ProductId is null)
            {
                return BadRequest("The productId can not be null");
            }

            var existingProduct = await _productRepository.GetById(product.ProductId);
            if (existingProduct is null)
            {
                return BadRequest("The product doesn't exist");
            }

            await _productRepository.ReplaceOne(product);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchProduct(string id, [FromBody] JsonPatchDocument<Product> patchDoc)
        {
            try
            {
                if(patchDoc is not null)
                {
                    var product = await _productRepository.GetById(id);
                    if(product is null)
                    {
                        return NotFound();
                    }

                    patchDoc.ApplyTo(product,HandleJsonPatchError);

                    await _productRepository.ReplaceOne(product);
                    return NoContent();

                }

                return BadRequest();
            }
            catch (System.Exception ex)
            {
                 return BadRequest(ex.Message);
            }
        }

        private void HandleJsonPatchError(JsonPatchError jsonPatchError)
        {
            throw new Exception(jsonPatchError.ErrorMessage);
        }
    }
}