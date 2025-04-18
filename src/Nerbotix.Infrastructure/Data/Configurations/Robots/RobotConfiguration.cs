﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerbotix.Domain.Robots;

namespace Nerbotix.Infrastructure.Data.Configurations.Robots;

public class RobotConfiguration : IEntityTypeConfiguration<Robot>
{
    public void Configure(EntityTypeBuilder<Robot> builder)
    {
        builder.ToTable("robots");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name).IsRequired();
        builder.HasIndex(x => new { x.TenantId, x.Name }).IsUnique();
        
        builder.HasOne(r => r.Communication)
            .WithOne(c => c.Robot)
            .HasForeignKey<RobotCommunication>(c => c.RobotId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(r => r.Properties)
            .WithOne(p => p.Robot)
            .HasForeignKey(p => p.RobotId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(r => r.CustomProperties)
            .WithOne(p => p.Robot)
            .HasForeignKey(p => p.RobotId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(r => r.Capabilities)
            .WithOne(p => p.Robot)
            .HasForeignKey(p => p.RobotId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(r => r.TasksQueue)
            .WithOne(t => t.AssignedRobot)
            .HasForeignKey(t => t.AssignedRobotId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
        
        builder.HasMany(r => r.Logs)
            .WithOne(l => l.Robot)
            .HasForeignKey(l => l.RobotId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder.OwnsOne(r => r.Location);
    }
}