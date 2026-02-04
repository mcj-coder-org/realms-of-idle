namespace RealmsOfIdle.Core.Domain;

using System;
using System.Collections.Generic;
using RealmsOfIdle.Core.Domain.Models;

public class PlayerProfile
{
    public string PlayerId { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public DateTime FirstLogin { get; set; } = DateTime.UtcNow;
    public DateTime LastLogin { get; set; } = DateTime.UtcNow;
    public int TotalPlayTimeMinutes { get; set; }
    public GameStats Statistics { get; set; } = new();
    public Dictionary<string, object> Preferences { get; set; } = new();
}