namespace RealmsOfIdle.Core.Scenarios.Inn;

/// <summary>
/// Represents a facility in the Inn (Kitchen, Bar, Guest Room, Staff Bed, etc.)
/// </summary>
public sealed record class InnFacility(
    string Type,
    int Level = 1,
    int Capacity = 1,
    double ProductionRate = 1.0,
    int UpgradeCost = 100)
{
    /// <summary>
    /// Upgrades the facility to the next level
    /// </summary>
    public InnFacility Upgrade()
    {
        // Each level increases production rate by 20% and upgrade cost by 50%
        var newLevel = Level + 1;
        var newProductionRate = ProductionRate * 1.2;
        var newUpgradeCost = (int)(UpgradeCost * 1.5);
        var newCapacity = Capacity + 1;

        return new InnFacility(Type, newLevel, newCapacity, newProductionRate, newUpgradeCost);
    }

    /// <summary>
    /// Checks if the facility is at capacity
    /// </summary>
    public bool IsAtCapacity(int currentCount) => currentCount >= Capacity;
}
