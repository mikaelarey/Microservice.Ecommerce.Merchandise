using Merchandise.Domain.Models.Aggregates;
using Merchandise.Domain.Models.ReferenceModels;
using Merchandise.Infrastructure.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Merchandise.Infrastructure.Persistences
{
    public class MerchandiseDbContext : DbContext
    {
        public MerchandiseDbContext(DbContextOptions<MerchandiseDbContext> options) 
            : base(options) { }
        
        public DbSet<Product> Product { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<AttributeName> AttributeName { get; set; }
        public DbSet<AttributeValue> AttributeValue { get; set; }
        public DbSet<Variant> Variant { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<UserAccount> UserAccount { get; set; }
        public DbSet<ProductVariant> ProductVariant { get; set; }
        public DbSet<ProductAttribute> ProductAttribute { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductImageEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BrandEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AttributeNameEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AttributeValueEntityConfiguration());
            modelBuilder.ApplyConfiguration(new VariantEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductVariantEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductAttributeEntityConfiguration());
        }
    }
}
