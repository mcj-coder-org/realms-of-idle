namespace RealmsOfIdle.Core.Domain;

using RealmsOfIdle.Core.Domain.Models;

public class PlayerState
{
    public string PlayerId { get; }
    public string Name { get; }
    public int Level { get; }
    public int Experience { get; }
    public GameMode CurrentMode { get; }

    public PlayerState(string playerId, string name, int level = 1, int experience = 0, GameMode currentMode = default!)
    {
        PlayerId = playerId;
        Name = name;
        Level = level;
        Experience = experience;
        CurrentMode = currentMode;
    }
}
