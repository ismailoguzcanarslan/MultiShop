using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ProductImageUrl { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        [BsonIgnore] //db ye kaydetmez
        public Category Category { get; set; }
    }
}
