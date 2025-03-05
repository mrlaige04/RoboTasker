﻿using RoboTasker.Application.Common.Abstractions;

namespace RoboTasker.Application.Robots.Capabilities;

public class CapabilityBaseResponse : TenantEntityResponse
{
    public string GroupName { get; set; } = null!;
    public string? Description { get; set; }
    public int CapabilitiesCount { get; set; }
}