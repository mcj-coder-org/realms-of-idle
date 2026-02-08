namespace RealmsOfIdle.Core.Scenarios.Inn;

/// <summary>
/// Represents a task assigned to a staff member
/// </summary>
public sealed record class StaffTask(
    StaffTaskType Type,
    string? TargetId = null,
    double Progress = 0.0)
{
    /// <summary>
    /// Gets whether the task is complete
    /// </summary>
    public bool IsComplete => Progress >= 1.0;
}
