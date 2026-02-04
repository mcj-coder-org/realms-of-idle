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
}