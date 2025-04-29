namespace Merchandise.Domain.DataModels.Products
{
    public class ProductUpdateRequestDataModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid BrandId { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DateTimeLastUpdated { get; set; }
    }
}
