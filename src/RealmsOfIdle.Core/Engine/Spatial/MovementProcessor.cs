namespace RealmsOfIdle.Core.Engine.Spatial;

/// <summary>
/// Handles entity movement and travel progress in the scene graph
/// </summary>
public static class MovementProcessor
{
    /// <summary>
    /// Processes movement for an entity over a given time delta
    /// </summary>
    /// <param name="position">The current entity position</param>
    /// <param name="graph">The scene graph for travel time calculations</param>
    /// <param name="speed">The movement speed multiplier (1.0 = normal, 2.0 = double speed)</param>
    /// <param name="deltaTime">The time elapsed in seconds</param>
    /// <returns>The updated entity position</returns>
    public static EntityPosition ProcessMovement(EntityPosition position, SceneGraph graph, double speed, double deltaTime)
    {
        ArgumentNullException.ThrowIfNull(position);
        ArgumentNullException.ThrowIfNull(graph);
        ArgumentOutOfRangeException.ThrowIfNegative(speed);
        ArgumentOutOfRangeException.ThrowIfNegative(deltaTime);

        // If not traveling, return the same position
        if (!position.IsTraveling || position.TargetNode == null)
        {
            return position;
        }

        // Validate current node exists
        if (graph.GetNode(position.CurrentNode) == null)
        {
            throw new ArgumentException($"Current node '{position.CurrentNode}' not found in scene graph.");
        }

        // Calculate travel time
        var travelTime = CalculateTravelTime(position.CurrentNode, position.TargetNode, graph, speed);

        if (travelTime <= 0)
        {
            // Instant travel
            return position.WithTravelProgress(1.0);
        }

        // Calculate progress increment
        var progressIncrement = deltaTime / travelTime;
        var newProgress = position.TravelProgress + progressIncrement;

        return position.WithTravelProgress(newProgress);
    }

    /// <summary>
    /// Calculates the travel time between two nodes
    /// </summary>
    /// <param name="fromNode">The starting node name</param>
    /// <param name="toNode">The destination node name</param>
    /// <param name="graph">The scene graph containing the nodes</param>
    /// <param name="speed">The movement speed multiplier</param>
    /// <returns>The travel time in seconds (or -1 if nodes not found)</returns>
    public static double CalculateTravelTime(string fromNode, string toNode, SceneGraph graph, double speed)
    {
        ArgumentNullException.ThrowIfNull(graph);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(speed);

        var distance = graph.CalculateTravelTime(fromNode, toNode);
        if (distance < 0)
        {
            return -1;
        }

        // Travel time is distance divided by speed
        return distance / speed;
    }
}
