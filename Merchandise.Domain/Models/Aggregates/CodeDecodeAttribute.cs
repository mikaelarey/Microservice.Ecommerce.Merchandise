using Merchandise.Domain.Models.Common;

namespace Merchandise.Domain.Models.Aggregates
{
    public class CodeDecodeAttribute : BaseEntity
    {
        public int Code { get; set; }
        public string Name { get; private set; } = string.Empty;
        public string Decode { get; set; }

        public CodeDecodeAttribute() { }

        public CodeDecodeAttribute(int code, string name, string decode) : base()
        {
            Code = code;
            Name = name;
            Decode = decode;
        }
    }
}
