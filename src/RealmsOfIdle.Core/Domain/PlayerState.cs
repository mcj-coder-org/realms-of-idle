namespace RealmsOfIdle.Core.Domain;

public readonly record PlayerState(
    string PlayerId,
    string Name,
    int Level = 1,
    int Experience = 0,
    GameMode CurrentMode = GameMode.Offline);