
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WashingtonStoreWebApi.Configurations;
using WashingtonStoreWebApi.Models;
using WashingtonStoreWebApi.Validators;
using static System.Console;

namespace WashingtonStoreWebApi.Infrastructure.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> productsMongoCollection;

        public ProductRepository(IOptions<StoreDbSettings> storeDbSettings)
        {
            var mongoClient = new MongoClient(storeDbSettings.Value.ConnectionString);
            var storeDatabase = mongoClient.GetDatabase(storeDbSettings.Value.DatabaseName); 
            productsMongoCollection = storeDatabase.GetCollection<Product>(storeDbSettings.Value.ProductsCollectionName);
        }

        public async Task InsertOne(Product product)
        {
            var validationErrors = product.Validate();
            if(validationErrors.Any())
            {
                foreach(var error in validationErrors)
                {
                    WriteLine(error.ErrorMessage);
                }
            }

            await productsMongoCollection.InsertOneAsync(product);
        }

        public async Task<List<Product>> Search(string searchSentence)
        {
           var searchFilterDefinition = Builders<Product>.Filter.Text(searchSentence);
           var projectionDefinition = Builders<Product>.Projection
                .Include(p=>p.Name)
                .Include(p=>p.Description)
                .Include(p=>p.Price)
                .MetaTextScore("relevance");
            var sortDefinition = Builders<Product>.Sort.MetaTextScore("relevance");

           var products = await productsMongoCollection
           .Find(searchFilterDefinition)
           .Project<Product>(projectionDefinition)
           .Sort(sortDefinition)
           .ToListAsync();
           return products;
        }

        public async Task<Product> GetById(string id)
        {
            var filterDefinition = Builders<Product>.Filter.Eq(p=>p.ProductId,id);
            return await productsMongoCollection.Find<Product>(filterDefinition).FirstOrDefaultAsync();
        }
    }
}