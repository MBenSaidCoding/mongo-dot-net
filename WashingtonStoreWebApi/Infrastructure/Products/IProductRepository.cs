using WashingtonStoreWebApi.Models;

namespace WashingtonStoreWebApi.Infrastructure.Products
{
    public interface IProductRepository
    {
        Task<Product> GetById(string id);

        Task<List<Product>> Search(string searchSentence);

        Task InsertOne (Product product);
    }
}