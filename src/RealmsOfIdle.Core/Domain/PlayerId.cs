namespace RealmsOfIdle.Core.Domain;

public readonly record PlayerId(string Value)
{
    public static PlayerId New() => new(Guid.NewGuid().ToString());
}