﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoboTasker.Domain.Robots;
using RoboTasker.Domain.Tenants;

namespace RoboTasker.Infrastructure.Data;

public class RoboTaskerDbContext(DbContextOptions<RoboTaskerDbContext> options) 
    : IdentityDbContext<User, Role, Guid>(options)
{
    public DbSet<Tenant> Tenants { get; set; } = null!;

    public DbSet<Robot> Robots { get; set; } = null!;
    public DbSet<RobotCategory> RobotCategories { get; set; } = null!;
    public DbSet<RobotCommunication> RobotCommunications { get; set; } = null!;
    public DbSet<RobotCustomProperty> RobotCustomProperties { get; set; } = null!;
    public DbSet<RobotProperty> RobotProperties { get; set; } = null!;
    public DbSet<RobotPropertyValue> RobotPropertyValues { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}