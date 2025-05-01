using Merchandise.Domain.Enums;
using Merchandise.Domain.Extensions;
using Merchandise.Domain.Models.Aggregates;
using Merchandise.Infrastructure.Persistences;

namespace Merchandise.Infrastructure.Seeder
{
    public class CodeDecodeAttributeSeeder
    {
        public static void Seed(MerchandiseDbContext context)
        {
            var attributes = Enum.GetValues(typeof(CodeAttribute))
                .Cast<CodeAttribute>()
                .Select(attr => new CodeDecodeAttribute((int)attr, attr.ToString(), attr.GetDescription()))
                .ToList();

            foreach (var attr in attributes)
            {
                if (!context.CodeDecodeAttribute.Any(a => a.Code == attr.Code))
                {
                    context.CodeDecodeAttribute.Add(attr);
                }
            }

            context.SaveChanges();
        }
    }
}
