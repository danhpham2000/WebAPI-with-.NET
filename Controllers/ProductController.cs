using Microsoft.AspNetCore.Mvc;
using MongoDemo.Models;
using MongoDemo.Services;

namespace MongoDemo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : Controller
    {
        private readonly IProductService _productService = productService;
        
        // GET api/Product
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }
        
        // GET api/Product/:id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var product = await _productService.GetById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }
        
        // POST
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            await _productService.CreateAsync(product);
            return Ok("Created ok");
        }
        
        // UPDATE api/Product/:id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Product newProduct)
        {
            var product = await _productService.GetById(id);
            if (product == null) return NotFound();
            
            await _productService.UpdateAsync(id, newProduct);
            return Ok("Update ok");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _productService.DeleteAsync(id);
            return Ok("Delete ok");
        }
    }
}