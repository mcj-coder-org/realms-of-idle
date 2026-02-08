namespace RealmsOfIdle.Core.Engine.Spatial;

/// <summary>
/// Represents a position on a 2D grid using integer coordinates
/// </summary>
public readonly record struct GridPosition(int X, int Y)
{
    /// <summary>
    /// Gets a string representation of this position
    /// </summary>
    public override string ToString() => $"({X}, {Y})";

    /// <summary>
    /// Adds another position to this position
    /// </summary>
    public GridPosition Add(GridPosition other) => new(X + other.X, Y + other.Y);

    /// <summary>
    /// Calculates the Manhattan distance to another position
    /// </summary>
    public int ManhattanDistanceTo(GridPosition other) =>
        Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

    /// <summary>
    /// Gets the four adjacent positions (up, down, left, right)
    /// </summary>
    public IReadOnlyList<GridPosition> AdjacentPositions =>
    [
        new GridPosition(X - 1, Y), // Left
        new GridPosition(X + 1, Y), // Right
        new GridPosition(X, Y - 1), // Up
        new GridPosition(X, Y + 1)  // Down
    ];
}
