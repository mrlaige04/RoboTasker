﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoboTasker.Domain.Capabilities;

namespace RoboTasker.Infrastructure.Data.Configurations.Capabilities;

public class RobotCapabilityConfiguration : IEntityTypeConfiguration<RobotCapability>
{
    public void Configure(EntityTypeBuilder<RobotCapability> builder)
    {
        builder.ToTable("robots_capabilities");
        builder.HasKey(c => new { c.RobotId, c.CapabilityId });
    }
}