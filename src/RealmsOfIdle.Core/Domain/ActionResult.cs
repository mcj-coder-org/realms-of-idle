namespace RealmsOfIdle.Core.Domain;

public readonly record ActionResult(
    bool Success,
    string? Message = null,
    IReadOnlyList<GameEvent>? Events = null)
{
    public static ActionResult Ok(string message = "Success") =>
        new(true, message);

    public static ActionResult Fail(string message) =>
        new(false, message);
}