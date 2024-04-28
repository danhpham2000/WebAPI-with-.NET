using Microsoft.AspNetCore.Mvc;
using MongoDemo.Models;
using MongoDemo.Services;

namespace MongoDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : Controller
    {
        private readonly ICategoryService _categoryService = categoryService;

        // GET: api/Category/
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        // GET: api/Category/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var category = await _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        
        // POST: api/Category
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category category)
        {
            await _categoryService.CreateAsync(category);
            return Ok("Created ok!");
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Category newCategory)
        {
            var category = await _categoryService.GetById(id);
            if (category == null) return NotFound();
            await _categoryService.UpdateAsync(id, newCategory);
            return Ok("Updated ok!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var category = await _categoryService.GetById(id);
            if (category == null) return NotFound();
            await _categoryService.DeleteAsync(id);
            return Ok("Deleted ok!");
        }
        
        

    }
}