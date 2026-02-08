namespace RealmsOfIdle.Core.Domain;

using System.Collections.Generic;

public class ActionResult
{
    public bool Success { get; }
    public string? Message { get; }
    public IReadOnlyList<GameEvent>? Events { get; }

    public ActionResult(bool success, string? message = null, IReadOnlyList<GameEvent>? events = null)
    {
        Success = success;
        Message = message;
        Events = events;
    }

    public static ActionResult Ok(string message = "Success") =>
        new(true, message);

    public static ActionResult Ok(string message, IReadOnlyList<GameEvent> events) =>
        new(true, message, events);

    public static ActionResult Fail(string message) =>
        new(false, message);
}
