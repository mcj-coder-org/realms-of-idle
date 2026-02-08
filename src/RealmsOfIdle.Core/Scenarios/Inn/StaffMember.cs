using RealmsOfIdle.Core.Engine.Spatial;

namespace RealmsOfIdle.Core.Scenarios.Inn;

/// <summary>
/// Represents a staff member working at the Inn
/// </summary>
public sealed record class StaffMember(
    string Name,
    string Role,
    StaffTask? CurrentTask = null,
    double Efficiency = 1.0,
    EntityPosition? Position = null,
    GridPosition? DesignatedBed = null,
    double Fatigue = 0.0)
{
    /// <summary>
    /// Gets whether the staff member is idle (no task assigned)
    /// </summary>
    public bool IsIdle => CurrentTask == null || CurrentTask.Type == StaffTaskType.None;

    /// <summary>
    /// Gets whether the staff member needs to sleep (fatigue >= 70%)
    /// </summary>
    public bool NeedsSleep => Fatigue >= 0.7;

    /// <summary>
    /// Creates a new staff member with a task assigned
    /// </summary>
    public StaffMember WithTask(StaffTask task)
    {
        return this with { CurrentTask = task };
    }

    /// <summary>
    /// Creates a new staff member with the task cleared
    /// </summary>
    public StaffMember ClearTask()
    {
        return this with { CurrentTask = null };
    }

    /// <summary>
    /// Creates a new staff member with an updated position
    /// </summary>
    public StaffMember WithPosition(EntityPosition position)
    {
        return this with { Position = position };
    }

    /// <summary>
    /// Creates a new staff member with updated efficiency
    /// </summary>
    public StaffMember WithEfficiency(double efficiency)
    {
        return this with { Efficiency = efficiency };
    }

    /// <summary>
    /// Creates a new staff member with increased fatigue
    /// </summary>
    public StaffMember IncreaseFatigue(double amount)
    {
        var newFatigue = Math.Min(1.0, Fatigue + amount);
        return this with { Fatigue = newFatigue };
    }

    /// <summary>
    /// Creates a new staff member with decreased fatigue
    /// </summary>
    public StaffMember DecreaseFatigue(double amount)
    {
        var newFatigue = Math.Max(0.0, Fatigue - amount);
        return this with { Fatigue = newFatigue };
    }

    /// <summary>
    /// Creates a new staff member with fatigue set to a specific value
    /// </summary>
    public StaffMember WithFatigue(double fatigue)
    {
        return this with { Fatigue = Math.Clamp(fatigue, 0.0, 1.0) };
    }
}
