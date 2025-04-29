using Merchandise.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Merchandise.Infrastructure.EntityConfiguration
{
    internal class ProductImageEntityConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.ImageUrl).IsRequired().HasMaxLength(5000);
            builder.Property(p => p.DateTimeLastUpdated).IsConcurrencyToken();

            builder.HasIndex(p => p.ProductId).HasDatabaseName("IX_ProductImage_ProductId");
        }
    }
}
