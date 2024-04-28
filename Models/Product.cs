using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDemo.Models;

public class Product
{
    // Create the schema for the model to interact with MongoDB
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    // Primary key
    public string Id { get; set; } = null!;

    public string ProductName { get; set; } = null!;
    
    [BsonRepresentation(BsonType.ObjectId)]
    public string CategoryId { get; set; } = null!;

    [BsonIgnoreIfNull] public string CategoryName { get; set; } = null!;
}