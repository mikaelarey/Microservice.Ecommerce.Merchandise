namespace Merchandise.Domain.DataModels.Products
{
    public class ProductQueryResultDataModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
