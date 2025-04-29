using Merchandise.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Merchandise.Infrastructure.EntityConfiguration
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Key
            builder.HasKey(p => p.Id);

            // Properties
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).HasMaxLength(10000);
            builder.Property(p => p.DateTimeLastUpdated).IsConcurrencyToken();

            // Indexes
            builder.HasIndex(p => p.Name).IsUnique();
        }
    }
}
