﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerbotix.Domain.Robots;

namespace Nerbotix.Infrastructure.Data.Configurations.Robots;

public class RobotCategoryConfiguration : IEntityTypeConfiguration<RobotCategory>
{
    public void Configure(EntityTypeBuilder<RobotCategory> builder)
    {
        builder.ToTable("robot_categories");
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Name).IsRequired();
        builder.HasIndex(c => new { c.TenantId, c.Name }).IsUnique();
        
        builder.HasMany(c => c.Robots)
            .WithOne(r => r.Category)
            .HasForeignKey(r => r.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(c => c.Properties)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}