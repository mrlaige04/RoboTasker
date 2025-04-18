﻿using Nerbotix.Domain.Abstractions;

namespace Nerbotix.Domain.Robots;

public class RobotCustomProperty : TenantEntity
{
    public string Name { get; set; } = null!;
    public string Value { get; set; } = null!;
    
    public Robot Robot { get; set; } = null!;
    public Guid RobotId { get; set; }
}