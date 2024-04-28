using MongoDemo.Models;

namespace MongoDemo.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetById(string id);
    Task CreateAsync(Product product);
    Task UpdateAsync(string id, Product product);
    Task DeleteAsync(string id);
}