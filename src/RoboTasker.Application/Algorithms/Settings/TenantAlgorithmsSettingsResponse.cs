﻿using RoboTasker.Application.Common.Abstractions;

namespace RoboTasker.Application.Algorithms.Settings;

public class TenantAlgorithmsSettingsResponse : TenantEntityResponse
{
    public AntColonySettingsResponse AntColony { get; set; } = null!;
    public GeneticSettingsResponse Genetic { get; set; } = null!;
    public LoadBalancingSettingsResponse LoadBalancing { get; set; } = null!;
    public SimulatedAnnealingSettingsResponse SimulatedAnnealing { get; set; } = null!;
}