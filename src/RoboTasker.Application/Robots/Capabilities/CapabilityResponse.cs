﻿namespace RoboTasker.Application.Robots.Capabilities;

public class CapabilityResponse : CapabilityBaseResponse
{
    public IList<CapabilityItemResponse> Capabilities { get; set; } = [];
}