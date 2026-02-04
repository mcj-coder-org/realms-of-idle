namespace RealmsOfIdle.Core.Core.Exceptions;

/// <summary>
/// Exception thrown when there's a concurrency conflict in event storage
/// </summary>
public class ConcurrencyException : Exception
{
    public ConcurrencyException(string message) : base(message) { }

    public ConcurrencyException(string message, Exception innerException) : base(message, innerException) { }

    public ConcurrencyException()
    {
    }
}
