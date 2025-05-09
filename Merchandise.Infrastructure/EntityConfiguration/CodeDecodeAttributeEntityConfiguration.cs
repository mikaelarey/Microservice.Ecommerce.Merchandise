﻿using Merchandise.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Merchandise.Infrastructure.EntityConfiguration
{
    public class CodeDecodeAttributeEntityConfiguration : IEntityTypeConfiguration<CodeDecodeAttribute>
    {
        public void Configure(EntityTypeBuilder<CodeDecodeAttribute> builder)
        {
            // Key
            builder.HasKey(p => p.Id);

            // Properties
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.DateTimeLastUpdated).IsConcurrencyToken();

            // Indexes
            builder.HasIndex(p => p.Name).IsUnique();
            builder.HasIndex(p => p.Code).IsUnique();
        }
    }
}
