using RealmsOfIdle.Client.Blazor.Models;
using RealmsOfIdle.Client.Blazor.Services;
using RealmsOfIdle.Client.Blazor.Tests.Fixtures;

namespace RealmsOfIdle.Client.Blazor.Tests.Services;

[Trait("Category", "Unit")]
public class OfflineProgressCalculatorTests
{
    private readonly OfflineProgressCalculator _calculator = new();

    // T130: <60s elapsed → no calculation (returns unchanged settlement)
    [Theory]
    [InlineData(0)]
    [InlineData(30)]
    [InlineData(59)]
    public void CalculateProgress_WithLessThan60Seconds_ReturnsNoProgress(int seconds)
    {
        var settlement = SettlementTestFixtures.CreateTestSettlement();
        var elapsed = TimeSpan.FromSeconds(seconds);

        var result = _calculator.CalculateProgress(settlement, elapsed);

        Assert.Equal(0, result.TotalGoldEarned);
        Assert.Equal(0, result.TotalReputationEarned);
        Assert.Equal(0, result.TotalActionsPerformed);
        Assert.Equal(TimeSpan.Zero, result.CappedElapsedTime);
        Assert.Same(settlement, result.UpdatedSettlement);
    }

    // T131: 5 minutes elapsed → correct action cycles
    [Fact]
    public void CalculateProgress_With5Minutes_CalculatesCorrectCycles()
    {
        var settlement = SettlementTestFixtures.CreateTestSettlement();
        var elapsed = TimeSpan.FromMinutes(5); // 300 seconds

        var result = _calculator.CalculateProgress(settlement, elapsed);

        // Mara (Innkeeper): ServeCustomer = 5s, so 300/5 = 60 cycles × 5 gold = 300 gold
        // Tomas (Blacksmith): CraftSword = 30s, so 300/30 = 10 cycles × 20 gold = 200 gold
        // Cook: ProduceFood has no gold reward
        // Customer: Rest has no gold reward
        Assert.True(result.TotalGoldEarned > 0);
        Assert.True(result.TotalActionsPerformed > 0);
        Assert.Equal(elapsed, result.CappedElapsedTime);
    }

    // T131 supplemental: Verify specific gold calculations for Innkeeper
    [Fact]
    public void CalculateProgress_With5Minutes_InnkeeperEarnsCorrectGold()
    {
        // Create settlement with only an innkeeper NPC to isolate
        var innkeeper = SettlementTestFixtures.CreateTestNPC("mara", NPCState.Idle, "Innkeeper", "inn");
        var settlement = SettlementTestFixtures.CreateTestSettlement(
            npcs: new List<NPC> { innkeeper });
        var elapsed = TimeSpan.FromMinutes(5); // 300 seconds

        var result = _calculator.CalculateProgress(settlement, elapsed);

        // ServeCustomer: 5s duration, 5 gold reward → 300/5 = 60 cycles → 60 × 5 = 300 gold
        Assert.Equal(300, result.TotalGoldEarned);
        Assert.Equal(60, result.TotalActionsPerformed);
        // Reputation: 60 × 2 = 120
        Assert.Equal(120, result.TotalReputationEarned);
    }

    // T132: 25 hours elapsed → 24hr cap applied
    [Fact]
    public void CalculateProgress_With25Hours_CapsAt24Hours()
    {
        var settlement = SettlementTestFixtures.CreateTestSettlement();
        var elapsed = TimeSpan.FromHours(25);

        var result = _calculator.CalculateProgress(settlement, elapsed);

        Assert.Equal(TimeSpan.FromHours(24), result.CappedElapsedTime);
        Assert.True(result.TotalGoldEarned > 0);
    }

    // T132 supplemental: Verify cap calculation is correct
    [Fact]
    public void CalculateProgress_With25Hours_CalculatesBasedOnCappedTime()
    {
        var innkeeper = SettlementTestFixtures.CreateTestNPC("mara", NPCState.Idle, "Innkeeper", "inn");
        var settlement = SettlementTestFixtures.CreateTestSettlement(
            npcs: new List<NPC> { innkeeper });
        var elapsed = TimeSpan.FromHours(25);

        var result = _calculator.CalculateProgress(settlement, elapsed);

        // Capped at 24hr = 86400 seconds
        // ServeCustomer: 5s → 86400/5 = 17280 cycles × 5 gold = 86400 gold
        Assert.Equal(86400, result.TotalGoldEarned);
        Assert.Equal(TimeSpan.FromHours(24), result.CappedElapsedTime);
    }

    // T133: Possessed NPC state is cleared
    [Fact]
    public void CalculateProgress_ClearsPossessedNPCState()
    {
        var settlement = SettlementTestFixtures.CreateTestSettlementWithPossessedNPC("mara");
        var elapsed = TimeSpan.FromMinutes(5);

        // Verify Mara is possessed before calculation
        var maraBefore = settlement.NPCs.First(n => n.Id == "mara");
        Assert.True(maraBefore.IsPossessed);

        var result = _calculator.CalculateProgress(settlement, elapsed);

        // After offline progress, Mara should no longer be possessed
        var maraAfter = result.UpdatedSettlement.NPCs.First(n => n.Id == "mara");
        Assert.False(maraAfter.IsPossessed);
        Assert.Equal(NPCState.Idle, maraAfter.State);
        Assert.Null(maraAfter.CurrentAction);
    }

    // T133 supplemental: Possessed NPC earns no rewards during offline
    [Fact]
    public void CalculateProgress_PossessedNPC_EarnsNoRewards()
    {
        // Create settlement with only a possessed innkeeper
        var innkeeper = SettlementTestFixtures.CreateTestNPC("mara", NPCState.Idle, "Innkeeper", "inn",
            isPossessed: true, gold: 100, reputation: 50);
        var settlement = SettlementTestFixtures.CreateTestSettlement(
            npcs: new List<NPC> { innkeeper });
        var elapsed = TimeSpan.FromMinutes(5);

        var result = _calculator.CalculateProgress(settlement, elapsed);

        // Possessed NPC: no gold earned
        Assert.Equal(0, result.TotalGoldEarned);
        Assert.Equal(0, result.TotalReputationEarned);
        Assert.Equal(0, result.TotalActionsPerformed);

        // NPC gold and reputation unchanged
        var mara = result.UpdatedSettlement.NPCs.First(n => n.Id == "mara");
        Assert.Equal(100, mara.Gold);
        Assert.Equal(50, mara.Reputation);
    }

    // T134: No resources → no actions for resource-dependent NPCs
    [Fact]
    public void CalculateProgress_WithNoResources_NoActionsForResourceDependentNPCs()
    {
        // Inn has no food → Innkeeper can't serve customers
        var settlement = SettlementTestFixtures.CreateTestSettlementWithNoResources();
        var elapsed = TimeSpan.FromMinutes(5);

        var result = _calculator.CalculateProgress(settlement, elapsed);

        // Innkeeper: ServeCustomer requires Food (0 available) → falls back to CheckIncome (no gold)
        // Blacksmith: CraftSword requires IronOre (0 available) → falls back to CheckMaterials (no gold)
        // Cook: ProduceFood requires no resources → still works but no gold reward
        // Customer: Rest → no gold reward
        Assert.Equal(0, result.TotalGoldEarned);
    }

    // T135: Multiple NPCs calculated independently
    [Fact]
    public void CalculateProgress_WithMultipleNPCs_CalculatesIndependently()
    {
        var settlement = SettlementTestFixtures.CreateTestSettlement();
        var elapsed = TimeSpan.FromMinutes(5); // 300 seconds

        var result = _calculator.CalculateProgress(settlement, elapsed);

        // Mara (Innkeeper): 300/5 = 60 actions, 60*5=300 gold, 60*2=120 rep
        // Tomas (Blacksmith): workshop has IronOre, 300/30 = 10 actions, 10*20=200 gold, 0 rep
        // Cook: ProduceFood has no gold/rep rewards
        // Customer: Rest has no gold/rep rewards
        var mara = result.UpdatedSettlement.NPCs.First(n => n.Id == "mara");
        var tomas = result.UpdatedSettlement.NPCs.First(n => n.Id == "tomas");

        // Mara earned reputation
        Assert.True(mara.Reputation > 50); // Started at 50

        // Total gold should be sum of all NPCs
        Assert.Equal(500, result.TotalGoldEarned); // 300 (innkeeper) + 200 (blacksmith)
        Assert.True(result.TotalActionsPerformed >= 70); // 60 + 10
    }

    // T136: Immutability verification - original settlement unchanged
    [Fact]
    public void CalculateProgress_DoesNotMutateOriginalSettlement()
    {
        var settlement = SettlementTestFixtures.CreateTestSettlement();
        var originalWorldTime = settlement.WorldTime;
        var originalMaraGold = settlement.NPCs.First(n => n.Id == "mara").Gold;
        var originalMaraRep = settlement.NPCs.First(n => n.Id == "mara").Reputation;
        var elapsed = TimeSpan.FromMinutes(5);

        var result = _calculator.CalculateProgress(settlement, elapsed);

        // Original settlement should be completely unchanged
        Assert.Equal(originalWorldTime, settlement.WorldTime);
        Assert.Equal(originalMaraGold, settlement.NPCs.First(n => n.Id == "mara").Gold);
        Assert.Equal(originalMaraRep, settlement.NPCs.First(n => n.Id == "mara").Reputation);

        // Result should be a different instance
        Assert.NotSame(settlement, result.UpdatedSettlement);
    }

    // T136 supplemental: Updated settlement has new WorldTime
    [Fact]
    public void CalculateProgress_UpdatesWorldTime()
    {
        var fixedTime = new DateTime(2026, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        var settlement = SettlementTestFixtures.CreateTestSettlement(worldTime: fixedTime);
        var elapsed = TimeSpan.FromMinutes(5);

        var result = _calculator.CalculateProgress(settlement, elapsed);

        // WorldTime should advance by the elapsed time
        Assert.Equal(fixedTime + elapsed, result.UpdatedSettlement.WorldTime);
    }

    // Edge case: Exactly 60 seconds should trigger calculation
    [Fact]
    public void CalculateProgress_WithExactly60Seconds_TriggersCalculation()
    {
        var innkeeper = SettlementTestFixtures.CreateTestNPC("mara", NPCState.Idle, "Innkeeper", "inn");
        var settlement = SettlementTestFixtures.CreateTestSettlement(
            npcs: new List<NPC> { innkeeper });
        var elapsed = TimeSpan.FromSeconds(60);

        var result = _calculator.CalculateProgress(settlement, elapsed);

        // 60/5 = 12 cycles × 5 gold = 60 gold
        Assert.Equal(60, result.TotalGoldEarned);
        Assert.Equal(12, result.TotalActionsPerformed);
    }

    // Edge case: NPC with Working state has action cleared
    [Fact]
    public void CalculateProgress_NPCInWorkingState_ReturnsToIdle()
    {
        var workingNPC = SettlementTestFixtures.CreateTestNPC("mara", NPCState.Working, "Innkeeper", "inn",
            currentAction: "serve_customer",
            actionStartTime: DateTime.UtcNow.AddSeconds(-3),
            actionDurationSeconds: 5);
        var settlement = SettlementTestFixtures.CreateTestSettlement(
            npcs: new List<NPC> { workingNPC });
        var elapsed = TimeSpan.FromMinutes(5);

        var result = _calculator.CalculateProgress(settlement, elapsed);

        var mara = result.UpdatedSettlement.NPCs.First(n => n.Id == "mara");
        Assert.Equal(NPCState.Idle, mara.State);
        Assert.Null(mara.CurrentAction);
        Assert.Null(mara.ActionStartTime);
        Assert.Null(mara.ActionDurationSeconds);
    }
}
