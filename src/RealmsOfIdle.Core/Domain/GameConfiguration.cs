namespace RealmsOfIdle.Core.Domain;

using System.Collections.Generic;

public class GameConfiguration
{
    public string GameMode { get; set; } = "Idle";
    public int StartingResources { get; set; } = 100;
    public Dictionary<string, int> ResourceRates { get; set; } = new();
    public int SaveIntervalSeconds { get; set; } = 60;
    public bool EnableEvents { get; set; } = true;
}