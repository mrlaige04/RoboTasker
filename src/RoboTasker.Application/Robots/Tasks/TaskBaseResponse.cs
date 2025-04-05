﻿using RoboTasker.Application.Common.Abstractions;
using RoboTasker.Domain.Tasks;

namespace RoboTasker.Application.Robots.Tasks;

public class TaskBaseResponse : TenantEntityResponse
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public RobotTaskStatus Status { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }
    
    public Guid? AssignedRobotId { get; set; }
    
    public TimeSpan? EstimatedDuration { get; set; }
    public int Priority { get; set; }
    public double Complexity { get; set; }
}