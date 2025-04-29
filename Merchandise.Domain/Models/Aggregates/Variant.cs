using Merchandise.Domain.Models.Common;

namespace Merchandise.Domain.Models.Aggregates
{
    public class Variant : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;

        public Variant() { }

        public Variant(string name) : base()
        {
            Name = name;
        }

        public void SetName(string name) 
        {
            Name = name;
        }

    }
}
