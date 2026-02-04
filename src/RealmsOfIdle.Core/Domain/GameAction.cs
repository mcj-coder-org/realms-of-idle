namespace RealmsOfIdle.Core.Domain;

using System.Collections.Generic;

public class GameAction
{
    public string ActionName { get; }
    public string? TargetId { get; }
    public Dictionary<string, object>? Parameters { get; }

    public GameAction(string actionName, string? targetId = null, Dictionary<string, object>? parameters = null)
    {
        ActionName = actionName;
        TargetId = targetId;
        Parameters = parameters;
    }
}
