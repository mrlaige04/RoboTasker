﻿using Nerbotix.Application.Common.Abstractions;

namespace Nerbotix.Application.Algorithms.Settings.SimulatedAnnealing;

public class UpdateSimulatedAnnealingSettingsCommand : ITenantCommand
{
    public double InitialTemperature { get; set; }
    public double CoolingRate { get; set; }
    public int IterationsPerTemp { get; set; }
    public double MinTemperature { get; set; }
}