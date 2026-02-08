namespace RealmsOfIdle.Core.Domain;

/// <summary>
/// Unique identifier for a player
/// </summary>
public readonly struct PlayerId : IEquatable<PlayerId>
{
    /// <summary>
    /// Gets the underlying string value of the player ID
    /// </summary>
    public readonly string Value { get; }

    /// <summary>
    /// Initializes a new instance of the PlayerId struct
    /// </summary>
    /// <param name="value">The unique identifier value</param>
    public PlayerId(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Returns the string representation of this player ID
    /// </summary>
    public override string ToString() => Value;

    /// <summary>
    /// Implicitly converts a PlayerId to its string value
    /// </summary>
    public static implicit operator string(PlayerId id) => id.Value;

    /// <summary>
    /// Explicitly converts a string to a PlayerId
    /// </summary>
    public static explicit operator PlayerId(string value) => new(value);

    /// <summary>
    /// Creates a PlayerId from a string value
    /// </summary>
    public static PlayerId FromString(string value) => new(value);

    /// <summary>
    /// Determines whether the specified object is equal to the current PlayerId
    /// </summary>
    public override bool Equals(object? obj)
    {
        return obj is PlayerId other && Equals(other);
    }

    /// <summary>
    /// Determines whether the specified PlayerId is equal to the current PlayerId
    /// </summary>
    public bool Equals(PlayerId other)
    {
        return string.Equals(Value, other.Value, StringComparison.Ordinal);
    }

    /// <summary>
    /// Returns a hash code for the current PlayerId
    /// </summary>
    public override int GetHashCode()
    {
        return Value?.GetHashCode(StringComparison.Ordinal) ?? 0;
    }

    /// <summary>
    /// Equality operator
    /// </summary>
    public static bool operator ==(PlayerId left, PlayerId right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Inequality operator
    /// </summary>
    public static bool operator !=(PlayerId left, PlayerId right)
    {
        return !(left == right);
    }
}
