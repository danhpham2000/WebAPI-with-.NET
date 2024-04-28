using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDemo.Models;

namespace MongoDemo.Services;

public class ProductService : IProductService
{
    private readonly IMongoCollection<Product> _productCollection;
    private readonly IOptions<DatabaseSettings> _databaseSettings;

    public ProductService(IOptions<DatabaseSettings> databaseSettings)
    {
        _databaseSettings = databaseSettings;
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _productCollection = mongoDatabase.GetCollection<Product>(
            databaseSettings.Value.ProductCollectionName);
    }
    
    // GET products
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        // Define the pipeline 
        var pipeline = new BsonDocument[]
        {
            new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "CategoryCollection" },
                { "localField", "CategoryId" },
                { "foreignField", "_id" },
                { "as", "product_category" }
            }),
            new BsonDocument("$unwind", "$product_category"),
            new BsonDocument("$project", new BsonDocument
            {
                {"_id", 1},
                {"CategoryId", 1},
                {"ProductName", 1},
                {"CategoryName", "$product_category.CategoryName"}
            })
        };
        var results = await _productCollection.Aggregate<Product>(pipeline).ToListAsync();
        
        return results;

    }
    
    // GET product by Id
    public async Task<Product> GetById(string id)
    {
        var product = await _productCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        return product;
    }
    
    // POST product
    public async Task CreateAsync(Product product)
    {
        await _productCollection.InsertOneAsync(product);
    }
    
    // UPDATE product
    public async Task UpdateAsync(string id, Product product)
    {
        await _productCollection.ReplaceOneAsync(p => p.Id == id, product);
    }

    public async Task DeleteAsync(string id)
    {
        await _productCollection.DeleteOneAsync(p => p.Id == id);
    }
    
}

