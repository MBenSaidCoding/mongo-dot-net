namespace WashingtonStoreWebApi.Configurations
{
    public class StoreDbSettings
    {
        public string ConnectionString {get; set;} =  null!;

        public string DatabaseName {get; set;} = null!;

        public string ProductsCollectionName {get; set;} = null!;

        public string CategoriesCollectionName {get; set;} = null!;
    }
}