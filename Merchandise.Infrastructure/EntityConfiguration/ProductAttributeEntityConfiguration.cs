using Merchandise.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Merchandise.Infrastructure.EntityConfiguration
{
    public class ProductAttributeEntityConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.DateTimeLastUpdated).IsConcurrencyToken();
            builder.HasIndex(x => x.ProductId).HasDatabaseName("IX_ProductAttribute_ProductId");
        }
    }
}
