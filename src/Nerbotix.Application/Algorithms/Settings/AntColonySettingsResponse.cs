﻿namespace Nerbotix.Application.Algorithms.Settings;

public class AntColonySettingsResponse
{
    public int AntCount { get; set; }
    public int Iterations { get; set; }
    public double Evaporation { get; set; }
    public double Alpha { get; set; }
    public double Beta { get; set; }
}