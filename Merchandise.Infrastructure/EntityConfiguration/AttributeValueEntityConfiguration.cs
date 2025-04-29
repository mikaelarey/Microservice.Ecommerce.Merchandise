using Merchandise.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Merchandise.Infrastructure.EntityConfiguration
{
    public class AttributeValueEntityConfiguration : IEntityTypeConfiguration<AttributeValue>
    {
        public void Configure(EntityTypeBuilder<AttributeValue> builder)
        {
            // Key
            builder.HasKey(p => p.Id);

            // Properties
            builder.Property(p => p.Value).IsRequired().HasMaxLength(100);
            builder.Property(p => p.DateTimeLastUpdated).IsConcurrencyToken();

            // Indexes
            builder.HasIndex(p => p.AttributeNameId).HasDatabaseName("IX_Attribute_Name_Id");
        }
    }
}
