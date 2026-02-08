using RealmsOfIdle.Core.Scenarios.Inn;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Scenarios.Inn;

/// <summary>
/// Unit tests for InnFacility
/// </summary>
[Trait("Category", "Unit")]
public class InnFacilityTests
{
    [Fact]
    public void Constructor_WithParameters_CreatesFacility()
    {
        // Arrange
        const string type = "Kitchen";
        const int level = 1;
        const int capacity = 5;
        const double productionRate = 1.0;
        const int upgradeCost = 100;

        // Act
        var facility = new InnFacility(type, level, capacity, productionRate, upgradeCost);

        // Assert
        Assert.Equal(type, facility.Type);
        Assert.Equal(level, facility.Level);
        Assert.Equal(capacity, facility.Capacity);
        Assert.Equal(productionRate, facility.ProductionRate);
        Assert.Equal(upgradeCost, facility.UpgradeCost);
    }

    [Fact]
    public void Constructor_DefaultLevel_UsesLevelOne()
    {
        // Arrange
        var facility = new InnFacility("Kitchen", 1, 5, 1.0, 100);

        // Act & Assert
        Assert.Equal(1, facility.Level);
    }

    [Fact]
    public void Upgrade_IncreasesLevel()
    {
        // Arrange
        var facility = new InnFacility("Kitchen", 1, 5, 1.0, 100);

        // Act
        var upgraded = facility.Upgrade();

        // Assert
        Assert.Equal(2, upgraded.Level);
    }

    [Fact]
    public void Upgrade_IncreasesProductionRate()
    {
        // Arrange
        var facility = new InnFacility("Kitchen", 1, 5, 1.0, 100);

        // Act
        var upgraded = facility.Upgrade();

        // Assert
        Assert.True(upgraded.ProductionRate > facility.ProductionRate);
    }

    [Fact]
    public void Upgrade_IncreasesUpgradeCost()
    {
        // Arrange
        var facility = new InnFacility("Kitchen", 1, 5, 1.0, 100);

        // Act
        var upgraded = facility.Upgrade();

        // Assert
        Assert.True(upgraded.UpgradeCost > facility.UpgradeCost);
    }

    [Fact]
    public void IsAtCapacity_WhenCurrentCountAtCapacity_ReturnsTrue()
    {
        // Arrange
        var facility = new InnFacility("GuestRoom", 1, 3, 0, 50);

        // Act
        var result = facility.IsAtCapacity(currentCount: 3);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsAtCapacity_WhenCurrentCountBelowCapacity_ReturnsFalse()
    {
        // Arrange
        var facility = new InnFacility("GuestRoom", 1, 3, 0, 50);

        // Act
        var result = facility.IsAtCapacity(currentCount: 2);

        // Assert
        Assert.False(result);
    }
}
