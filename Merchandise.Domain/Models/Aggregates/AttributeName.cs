using Merchandise.Domain.Models.Common;

namespace Merchandise.Domain.Models.Aggregates
{
    public class AttributeName : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;

        public AttributeName() { }

        public AttributeName(string name) : base()
        {
            Name = name;
        }
    }
}
