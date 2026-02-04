namespace RealmsOfIdle.Core.Domain;

using System;
using System.Collections.Generic;

public class GameStats
{
    public int TotalSessions { get; set; }
    public long TotalPlayTimeMs { get; set; }
    public int ActionsTaken { get; set; }
    public Dictionary<string, int> EventsByType { get; set; } = new();
    public Dictionary<string, int> ResourcesEarned { get; set; } = new();
    public int AchievementsUnlocked { get; set; }
    public DateTime FirstPlayed { get; set; }
    public DateTime LastPlayed { get; set; }
}