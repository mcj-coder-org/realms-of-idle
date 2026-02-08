namespace RealmsOfIdle.Core.Engine.Spatial;

/// <summary>
/// Represents the position of an entity in the scene graph with support for movement
/// </summary>
public sealed record class EntityPosition(string CurrentNode, string? TargetNode = null, double TravelProgress = 0.0)
{
    /// <summary>
    /// Gets whether the entity is currently traveling between nodes
    /// </summary>
    public bool IsTraveling => TargetNode != null && TravelProgress < 1.0;

    /// <summary>
    /// Gets whether the entity can perform actions (not traveling)
    /// </summary>
    public bool CanPerformActions => !IsTraveling;

    /// <summary>
    /// Creates a new position with updated travel progress
    /// </summary>
    public EntityPosition WithTravelProgress(double progress)
    {
        if (progress >= 1.0)
        {
            // Travel complete - move to target node
            return new EntityPosition(TargetNode ?? CurrentNode, null, 0.0);
        }

        return this with { TravelProgress = progress };
    }

    /// <summary>
    /// Starts traveling to a new target node
    /// </summary>
    public EntityPosition StartTravel(string? targetNode)
    {
        return new EntityPosition(CurrentNode, targetNode, 0.0);
    }
}
