namespace RealmsOfIdle.Core.Domain;

public readonly struct PlayerId
{
    public readonly string Value;

    public PlayerId(string value)
    {
        Value = value;
    }

    public override string ToString() => Value;

    public static implicit operator string(PlayerId id) => id.Value;
    public static explicit operator PlayerId(string value) => new(value);

    public static PlayerId ToPlayerId(PlayerId left, PlayerId right)
    {
        throw new NotImplementedException();
    }

    public override bool Equals(object obj)
    {
        throw new NotImplementedException();
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }

    public static bool operator ==(PlayerId left, PlayerId right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(PlayerId left, PlayerId right)
    {
        return !(left == right);
    }
}
