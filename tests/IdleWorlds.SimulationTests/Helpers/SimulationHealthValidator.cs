using AwesomeAssertions;
using Microsoft.Extensions.Logging;

namespace IdleWorlds.SimulationTests.Helpers;

/// <summary>
/// Defines expected outcomes for a simulation health validation.
/// </summary>
public class SimulationHealthExpectation
{
    public HashSet<Guid> ExpectedDeaths { get; } = new();
    public HashSet<string> ExpectedErrors { get; } = new();
    public decimal ExpectedCurrencyChange { get; set; }
    public Dictionary<Guid, TimeAllocation> ExpectedTimeAllocation { get; set; } = new();

    /// <summary>
    /// Creates a default healthy expectation (no deaths, no errors, balanced currency).
    /// </summary>
    public static SimulationHealthExpectation Healthy() => new();

    /// <summary>
    /// Expects a specific NPC to die during the simulation.
    /// </summary>
    public SimulationHealthExpectation WithExpectedDeath(Guid npcId)
    {
        ExpectedDeaths.Add(npcId);
        return this;
    }

    /// <summary>
    /// Expects an error message containing the specified text.
    /// </summary>
    public SimulationHealthExpectation WithExpectedError(string errorMessage)
    {
        ExpectedErrors.Add(errorMessage);
        return this;
    }
}

/// <summary>
/// Represents how an NPC allocates their time across activities.
/// </summary>
public record TimeAllocation(
    float Work = 0,
    float Rest = 0,
    float Social = 0,
    float Travel = 0,
    float Leisure = 0
);

/// <summary>
/// Result of running a simulation scenario.
/// </summary>
public class SimulationResult
{
    public required List<SimulationEvent> SimulationEvents { get; init; } = new();
    public required List<LogEntry> CapturedLogs { get; init; } = new();

    public IEnumerable<T> GetEvents<T>() where T : SimulationEvent =>
        SimulationEvents.OfType<T>();

    public NpcResult? GetNpcResult(Guid npcId) =>
        SimulationEvents
            .OfType<NpcTickEvent>()
            .FirstOrDefault(e => e.NpcId == npcId) as NpcResult;
}

/// <summary>
/// Base type for all simulation events.
/// </summary>
public abstract record SimulationEvent(DateTime Timestamp);

/// <summary>
/// Event representing an NPC death.
/// </summary>
public record NpcDeathEvent(DateTime Timestamp, Guid NpcId, string NpcName, string Cause) : SimulationEvent(Timestamp);

/// <summary>
/// Event representing currency creation/destruction.
/// </summary>
public record CurrencyEvent(DateTime Timestamp, Guid EntityId, string Source, decimal Amount) : SimulationEvent(Timestamp);

/// <summary>
/// Event representing an item creation/destruction/move.
/// </summary>
public record ItemEvent(DateTime Timestamp, Guid ItemId, string ItemType, ItemAction Action, Guid? OwnerId) : SimulationEvent(Timestamp);

/// <summary>
/// Event representing an NPC simulation tick.
/// </summary>
public record NpcTickEvent(DateTime Timestamp, Guid NpcId, string NpcName, TimeAllocation TimeSpent) : SimulationEvent(Timestamp);

/// <summary>
/// Result data for a specific NPC after simulation.
/// </summary>
public class NpcResult
{
    public required Guid NpcId { get; init; }
    public required string NpcName { get; init; }
    public required TimeAllocation TimeSpent { get; init; }
}

/// <summary>
/// Actions that can be performed on items.
/// </summary>
public enum ItemAction
{
    Created,
    Destroyed,
    Transferred,
    Consumed,
    Crafted
}

/// <summary>
/// Represents a log entry from simulation.
/// </summary>
public record LogEntry(LogLevel LogLevel, string Message);

/// <summary>
/// Validates simulation health by checking invariants and expectations.
/// </summary>
public static class SimulationHealthValidator
{
    /// <summary>
    /// Validates that a simulation result meets health expectations.
    /// </summary>
    /// <param name="result">The simulation result to validate.</param>
    /// <param name="expectation">Optional expectations for this simulation.</param>
    public static void ValidateSimulationHealth(
        SimulationResult result,
        SimulationHealthExpectation? expectation = null)
    {
        expectation ??= SimulationHealthExpectation.Healthy();

        var logs = result.CapturedLogs;
        var events = result.SimulationEvents;

        // No unexpected NPC deaths
        var deaths = events.OfType<NpcDeathEvent>().ToList();
        var unexpectedDeaths = deaths
            .Where(d => !expectation.ExpectedDeaths.Contains(d.NpcId))
            .ToList();

        unexpectedDeaths.Should().BeEmpty(
            $"NPCs died unexpectedly: {string.Join(", ", unexpectedDeaths.Select(d => d.NpcName))}");

        // Currency conservation
        var currencyEvents = events.OfType<CurrencyEvent>().ToList();
        var totalCreated = currencyEvents.Where(e => e.Amount > 0).Sum(e => e.Amount);
        var totalDestroyed = currencyEvents.Where(e => e.Amount < 0).Sum(e => Math.Abs(e.Amount));
        var expectedNetChange = expectation.ExpectedCurrencyChange;
        var actualNetChange = totalCreated - totalDestroyed;

        actualNetChange.Should().BeApproximately(expectedNetChange, tolerance: 0.01m,
            $"Currency conservation violated: created {totalCreated}, destroyed {totalDestroyed}, net change {actualNetChange}");

        // Item conservation - items should be accounted for
        var itemEvents = events.OfType<ItemEvent>().ToList();
        var created = itemEvents.Count(e => e.Action == ItemAction.Created);
        var destroyed = itemEvents.Count(e => e.Action == ItemAction.Destroyed || e.Action == ItemAction.Consumed);
        var transferred = itemEvents.Count(e => e.Action == ItemAction.Transferred);
        var crafted = itemEvents.Count(e => e.Action == ItemAction.Crafted);

        // Basic sanity check - transfers shouldn't exceed created + crafted
        transferred.Should().BeLessOrEqualTo(created + crafted,
            $"More items transferred ({transferred}) than created+crafted ({created + crafted})");

        // NPC time allocation (if expected)
        if (expectation.ExpectedTimeAllocation is not null)
        {
            foreach (var (npcId, expectedAllocation) in expectation.ExpectedTimeAllocation)
            {
                var npcResult = result.GetNpcResult(npcId);
                npcResult.Should().NotBeNull($"No result found for NPC {npcId}");
                npcResult!.TimeSpent.Should().MatchAllocation(expectedAllocation, 0.1f);
            }
        }

        // No unexpected errors in logs
        var unexpectedErrors = logs
            .Where(l => l.LogLevel == LogLevel.Error && !IsExpected(l.Message, expectation.ExpectedErrors))
            .Select(l => l.Message)
            .ToList();

        unexpectedErrors.Should().BeEmpty(
            $"Unexpected errors in logs: {string.Join("; ", unexpectedErrors)}");
    }

    private static bool IsExpected(string message, HashSet<string> expectedErrors)
    {
        return expectedErrors.Any(expected => message.Contains(expected, StringComparison.OrdinalIgnoreCase));
    }
}

/// <summary>
/// Extension methods for TimeAllocation assertions.
/// </summary>
public static class TimeAllocationExtensions
{
    /// <summary>
    /// Asserts that two time allocations match within tolerance.
    /// </summary>
    public static void ShouldMatchAllocation(
        this TimeAllocation actual,
        TimeAllocation expected,
        float tolerance = 0.05f)
    {
        actual.Work.Should().BeApproximately(expected.Work, tolerance);
        actual.Rest.Should().BeApproximately(expected.Rest, tolerance);
        actual.Social.Should().BeApproximately(expected.Social, tolerance);
        actual.Travel.Should().BeApproximately(expected.Travel, tolerance);
        actual.Leisure.Should().BeApproximately(expected.Leisure, tolerance);
    }

    /// <summary>
    /// Asserts that time allocations sum to approximately 1.0 (100%).
    /// </summary>
    public static void ShouldSumToUnity(this TimeAllocation allocation, float tolerance = 0.01f)
    {
        var sum = allocation.Work + allocation.Rest + allocation.Social + allocation.Travel + allocation.Leisure;
        sum.Should().BeApproximately(1.0f, tolerance, "Time allocation should sum to 100%");
    }
}
