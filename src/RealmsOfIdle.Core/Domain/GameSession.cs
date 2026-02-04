namespace RealmsOfIdle.Core.Domain;

using System;
using System.Collections.Generic;
using RealmsOfIdle.Core.Domain.Models;

public class GameSession
{
    public string SessionId { get; set; } = string.Empty;
    public string PlayerId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    public PlayerState PlayerState { get; set; } = new(string.Empty, "Player", 1, 0);
    public GameConfiguration Configuration { get; set; } = new();
    public bool IsActive { get; set; } = true;
}