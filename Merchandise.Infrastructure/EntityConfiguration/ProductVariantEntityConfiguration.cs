using Merchandise.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Merchandise.Infrastructure.EntityConfiguration
{
    public class ProductVariantEntityConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.DateTimeLastUpdated).IsConcurrencyToken();
            builder.HasIndex(p => new { p.ProductId }).HasDatabaseName("IX_ProductVariant_ProductId");
        }
    }
}
