using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WashingtonStoreWebApi.Models
{
    [BsonIgnoreExtraElements]
    public class Product
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? ProductId {get; set;}

        [BsonElement("name")]
        [Required]
        [MinLength(5, ErrorMessage ="Name must be at least 5 characters long.")]
        public string? Name {get; set;}

        [BsonElement("description")]
        [MinLength(10, ErrorMessage ="The description must be at least 10 characters long.")]
        public string? Description {get; set;}

        [BsonElement("price")]
        public decimal Price {get;set;}


        [BsonElement("relevance"), BsonIgnoreIfDefault]
        [JsonIgnore]
        public double Relevance {get; set;}
    }
}