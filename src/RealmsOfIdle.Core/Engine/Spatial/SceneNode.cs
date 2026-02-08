namespace RealmsOfIdle.Core.Engine.Spatial;

/// <summary>
/// Represents a named location in the scene with grid coordinates
/// </summary>
public sealed record class SceneNode(string Name, GridPosition Position, string AreaId = "")
{
    /// <summary>
    /// Determines equality based on name and position only
    /// </summary>
    public bool Equals(SceneNode? other)
    {
        if (other is null)
        {
            return false;
        }
        if (ReferenceEquals(this, other))
        {
            return true;
        }
        return Name == other.Name && Position.Equals(other.Position);
    }

    /// <summary>
    /// Returns hash code based on name and position only
    /// </summary>
    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Position);
    }
}
