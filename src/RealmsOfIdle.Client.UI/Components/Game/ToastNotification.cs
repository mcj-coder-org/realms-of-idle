namespace RealmsOfIdle.Client.UI.Components.Game;

/// <summary>
/// Toast notification
/// </summary>
public record ToastNotification
{
    public required string Message { get; init; }
    public required ToastType Type { get; init; }
    public required string Icon { get; init; }
    public DateTime Timestamp { get; init; }
}

/// <summary>
/// Toast notification type
/// </summary>
public enum ToastType
{
    Info,
    Success,
    Warning,
    Error
}
