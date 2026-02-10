using RealmsOfIdle.Client.Blazor.Models;
using RealmsOfIdle.Client.Blazor.Services;
using Xunit;

namespace RealmsOfIdle.Client.Blazor.Tests.Services;

/// <summary>
/// Unit tests for offline progress calculation.
/// Verifies TDD requirement: Tests MUST be written FIRST and FAIL before implementation.
/// </summary>
public sealed class OfflineProgressCalculatorTests
{
    private readonly OfflineProgressCalculator _calculator = new();
    private readonly ActionCatalog _actionCatalog = new();

    [Fact]
    public void CalculateProgress_LessThan60Seconds_ReturnsOriginalSettlement()
    {
        // Arrange
        var settlement = CreateTestSettlement();
        var elapsed = TimeSpan.FromSeconds(30);

        // Act
        var result = _calculator.CalculateProgress(settlement, elapsed, _actionCatalog);

        // Assert
        Assert.Equal(settlement.NPCs[0].Gold, result.UpdatedSettlement.NPCs[0].Gold);
        Assert.Equal(0, result.TotalGoldEarned);
        Assert.Empty(result.ActionsCompleted.Where(kvp => kvp.Value > 0));
    }

    [Fact]
    public void CalculateProgress_5Minutes_CalculatesCorrectCycles()
    {
        // Arrange
        var settlement = CreateTestSettlement();
        var elapsed = TimeSpan.FromMinutes(5); // 300 seconds
        var expectedCycles = 300 / 5; // ServeCustomer takes 5 seconds

        // Act
        var result = _calculator.CalculateProgress(settlement, elapsed, _actionCatalog);

        // Assert
        var mara = result.UpdatedSettlement.NPCs.First(n => n.Name == "Mara");
        Assert.Equal(settlement.NPCs[0].Gold + (expectedCycles * 5), mara.Gold); // +5 gold per action
        Assert.Equal(expectedCycles, result.ActionsCompleted["Mara"]);
    }

    [Fact]
    public void CalculateProgress_25Hours_CapsAt24Hours()
    {
        // Arrange
        var settlement = CreateTestSettlement();
        var elapsed = TimeSpan.FromHours(25);

        // Act
        var result = _calculator.CalculateProgress(settlement, elapsed, _actionCatalog);

        // Assert
        Assert.Equal(TimeSpan.FromHours(24), result.CappedElapsedTime);

        // Verify calculation used 24 hours, not 25
        var expectedCycles24h = (24 * 3600) / 5; // 24 hours in seconds / action duration
        var mara = result.UpdatedSettlement.NPCs.First(n => n.Name == "Mara");
        Assert.Equal(settlement.NPCs[0].Gold + (expectedCycles24h * 5), mara.Gold);
    }

    [Fact]
    public void CalculateProgress_PossessedNPC_ClearsPossessionState()
    {
        // Arrange
        var settlement = CreateTestSettlement();
        var possessedNPC = settlement.NPCs[0] with { IsPossessed = true };
        var updatedSettlement = settlement with
        {
            NPCs = new List<NPC> { possessedNPC }.Concat(settlement.NPCs.Skip(1)).ToList()
        };
        var elapsed = TimeSpan.FromMinutes(5);

        // Act
        var result = _calculator.CalculateProgress(updatedSettlement, elapsed, _actionCatalog);

        // Assert
        var mara = result.UpdatedSettlement.NPCs.First(n => n.Name == "Mara");
        Assert.False(mara.IsPossessed);
        Assert.Equal(NPCState.Idle, mara.State);
        Assert.Null(mara.CurrentAction);
    }

    [Fact]
    public void CalculateProgress_BlacksmithWithoutResources_NoActionsCompleted()
    {
        // Arrange
        var settlement = CreateTestSettlement();
        var workshop = settlement.Buildings.First(b => b.Type == BuildingType.Workshop);
        var emptyWorkshop = workshop with { Resources = new Dictionary<string, int>() }; // No iron ore
        var updatedSettlement = settlement with
        {
            Buildings = new List<Building> { settlement.Buildings[0], emptyWorkshop }
        };
        var elapsed = TimeSpan.FromMinutes(60);

        // Act
        var result = _calculator.CalculateProgress(updatedSettlement, elapsed, _actionCatalog);

        // Assert
        var tomas = result.UpdatedSettlement.NPCs.First(n => n.Name == "Tomas");
        Assert.Equal(settlement.NPCs[1].Gold, tomas.Gold); // No gold earned
        Assert.Equal(0, result.ActionsCompleted.GetValueOrDefault("Tomas", 0));
    }

    [Fact]
    public void CalculateProgress_MultipleNPCs_CalculatesIndependently()
    {
        // Arrange
        var settlement = CreateTestSettlement();
        var elapsed = TimeSpan.FromMinutes(10); // 600 seconds

        // Act
        var result = _calculator.CalculateProgress(settlement, elapsed, _actionCatalog);

        // Assert
        var mara = result.UpdatedSettlement.NPCs.First(n => n.Name == "Mara");
        var tomas = result.UpdatedSettlement.NPCs.First(n => n.Name == "Tomas");

        // Mara: 600s / 5s per action = 120 actions * 5 gold = 600 gold
        Assert.Equal(settlement.NPCs[0].Gold + 600, mara.Gold);

        // Tomas: 600s / 30s per action = 20 actions * 20 gold = 400 gold
        Assert.Equal(settlement.NPCs[1].Gold + 400, tomas.Gold);

        Assert.Equal(600 + 400, result.TotalGoldEarned);
    }

    [Fact]
    public void CalculateProgress_ImmutabilityVerification()
    {
        // Arrange
        var originalSettlement = CreateTestSettlement();
        var originalGold = originalSettlement.NPCs[0].Gold;
        var elapsed = TimeSpan.FromMinutes(5);

        // Act
        var result = _calculator.CalculateProgress(originalSettlement, elapsed, _actionCatalog);

        // Assert - Original settlement unchanged
        Assert.Equal(originalGold, originalSettlement.NPCs[0].Gold);
        Assert.NotEqual(originalSettlement, result.UpdatedSettlement);
        Assert.NotSame(originalSettlement.NPCs, result.UpdatedSettlement.NPCs);
    }

    private Settlement CreateTestSettlement()
    {
        return new Settlement(
            Id: "test-settlement",
            Name: "Test Settlement",
            Buildings: new List<Building>
            {
                new Building("inn", "Test Inn", BuildingType.Inn, new GridPosition(2, 2), 3, 3,
                    new Dictionary<string, int>()),
                new Building("workshop", "Test Forge", BuildingType.Workshop, new GridPosition(6, 2), 2, 2,
                    new Dictionary<string, int> { { "IronOre", 100 } })
            },
            NPCs: new List<NPC>
            {
                new NPC("mara", "Mara", "Innkeeper", 5, new GridPosition(3, 3), "inn",
                    NPCState.Idle, null, null, null, 100, 50, new Dictionary<string, object>(), false),
                new NPC("tomas", "Tomas", "Blacksmith", 8, new GridPosition(7, 3), "workshop",
                    NPCState.Idle, null, null, null, 200, 30, new Dictionary<string, object>(), false)
            },
            WorldTime: DateTime.UtcNow
        );
    }
}
