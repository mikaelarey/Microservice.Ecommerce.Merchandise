using Merchandise.Domain.Models.Aggregates;

namespace Merchandise.Domain.DataModels.Brands
{
    public class BrandAddResponseDataModel
    {
        public string Status { get; set; }
        public string? ErrorMessage { get; set; }
        public Brand? Brand { get; set; }
    }
}
