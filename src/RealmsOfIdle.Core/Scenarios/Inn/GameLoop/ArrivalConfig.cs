namespace RealmsOfIdle.Core.Scenarios.Inn;

/// <summary>
/// Configuration for customer arrival behavior
/// </summary>
public sealed record class ArrivalConfig(
    int MinReputation = 0,
    int MaxReputation = 100,
    int BaseInterval = 100,
    int MaxCapacity = 10)
{
}
