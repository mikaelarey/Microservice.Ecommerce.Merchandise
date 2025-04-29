using Merchandise.Domain.Models.Common;

namespace Merchandise.Domain.Models.Aggregates
{
    public class AttributeValue : BaseEntity
    {
        public Guid AttributeNameId { get; private set; }
        public string Value { get; private set; } = string.Empty;

        public AttributeValue() { }

        public AttributeValue(Guid attributeNameId, string value) : base()
        {
            AttributeNameId = attributeNameId;
            Value = value;
        }
    }
}
