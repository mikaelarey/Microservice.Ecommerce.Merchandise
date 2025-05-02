using Merchandise.Domain.Models.Aggregates;

namespace Merchandise.Domain.DataModels.Categories
{
    public class CategoryAddResponseDataModel
    {
        public string Status { get; set; }
        public string? ErrorMessage { get; set; }
        public Category? Category { get; set; }
    }
}
