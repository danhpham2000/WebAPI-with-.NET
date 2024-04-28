using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDemo.Models;

namespace MongoDemo.Services;

public class CategoryService : ICategoryService
{
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IOptions<DatabaseSettings> _databaseSettings;

    public CategoryService(IOptions<DatabaseSettings> databaseSettings)
    {
        _databaseSettings = databaseSettings;
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _categoryCollection = mongoDatabase.GetCollection<Category>
        (databaseSettings.Value.CategoryCollectionName);
    }
    
    // Get all the categories
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        var categories = await _categoryCollection.Find(_ => true).ToListAsync();
        return categories;
    }
    
    // Get category by Id
    public async Task<Category> GetById(string id)
    {
        var category = await _categoryCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
        return category;
    }
    
    // Create new category
    public async Task CreateAsync(Category category)
    {
        await _categoryCollection.InsertOneAsync(category);
    }
    
    // Update category
    public async Task UpdateAsync(string id, Category category)
    {
        await _categoryCollection.ReplaceOneAsync(
            c => c.Id == id, category);
    }
    
    // Delete category
    public async Task DeleteAsync(string id)
    {
        await _categoryCollection.DeleteOneAsync(c => c.Id == id);
    }
}




