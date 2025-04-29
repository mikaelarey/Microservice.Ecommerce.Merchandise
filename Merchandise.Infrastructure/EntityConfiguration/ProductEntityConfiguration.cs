using Merchandise.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Merchandise.Infrastructure.EntityConfiguration
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Key
            builder.HasKey(p => p.Id);

            // Properties
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.DateTimeLastUpdated).IsConcurrencyToken();
            builder.Property(p => p.DateTimeLastUpdated).IsConcurrencyToken();

            // Indexes
            builder.HasIndex(x => x.BrandId).HasDatabaseName("IX_ProductDetail_BrandId");
            builder.HasIndex(x => x.CategoryId).HasDatabaseName("IX_ProductDetail_CategoryId");
            builder.HasIndex(x => x.Price).HasDatabaseName("IX_ProductDetail_Price");

            // Multi Column Index
            // builder.HasIndex(p => new { p.Name, p.Price });

            // Naming Index
            // builder.HasIndex(p => p.Name).HasDatabaseName("IX_Product_Name");
        }
    }
}
