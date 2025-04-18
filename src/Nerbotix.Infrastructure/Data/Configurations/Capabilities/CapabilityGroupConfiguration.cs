﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerbotix.Domain.Capabilities;

namespace Nerbotix.Infrastructure.Data.Configurations.Capabilities;

public class CapabilityGroupConfiguration : IEntityTypeConfiguration<CapabilityGroup>
{
    public void Configure(EntityTypeBuilder<CapabilityGroup> builder)
    {
        builder.ToTable("capability_groups");
        builder.HasKey(cg => cg.Id);
        
        builder.HasMany(cg => cg.Capabilities)
            .WithOne(cg => cg.Group)
            .HasForeignKey(cg => cg.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}