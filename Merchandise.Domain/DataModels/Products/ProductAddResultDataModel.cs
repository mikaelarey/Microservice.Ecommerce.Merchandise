namespace Merchandise.Domain.DataModels.Products
{
    public class ProductAddResultDataModel
    {
        public Guid? Id { get; set; }
        public List<string>? ErrorMessage { get; set; }
        
    }
}
