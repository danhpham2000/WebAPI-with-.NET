using MongoDemo.Models;

namespace MongoDemo.Services;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category> GetById(string id);
    Task CreateAsync(Category category);
    Task UpdateAsync(string id, Category category);
    Task DeleteAsync(string id);

}