using Merchandise.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Merchandise.Infrastructure.EntityConfiguration
{
    public class VariantEntityConfiguration : IEntityTypeConfiguration<Variant>
    {
        public void Configure(EntityTypeBuilder<Variant> builder)
        {
            // Key
            builder.HasKey(p => p.Id);

            // Properties
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.DateTimeLastUpdated).IsConcurrencyToken();

            // Indexes
            builder.HasIndex(p => p.Name).IsUnique();
        }
    }
}
