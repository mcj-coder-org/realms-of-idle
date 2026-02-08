using RealmsOfIdle.Core.Domain;

namespace RealmsOfIdle.Core.Engine;

/// <summary>
/// Delegate for handling specific game actions
/// </summary>
/// <param name="state">The current game state</param>
/// <param name="action">The action to execute</param>
/// <param name="onComplete">Callback to invoke with the action result</param>
public delegate void ActionHandlerDelegate(GameState state, GameAction action, Action<ActionResult> onComplete);

/// <summary>
/// Validates and executes player actions
/// </summary>
public class ActionHandler
{
    private readonly Dictionary<string, ActionHandlerDelegate> _handlers;

    /// <summary>
    /// Initializes a new instance of ActionHandler
    /// </summary>
    public ActionHandler()
    {
        _handlers = new Dictionary<string, ActionHandlerDelegate>(StringComparer.OrdinalIgnoreCase);
        RegisterDefaultHandlers();
    }

    /// <summary>
    /// Registers a custom handler for a specific action type
    /// </summary>
    /// <param name="actionName">The name of the action</param>
    /// <param name="handler">The handler delegate</param>
    public void RegisterActionHandler(string actionName, ActionHandlerDelegate handler)
    {
        if (string.IsNullOrWhiteSpace(actionName))
        {
            throw new ArgumentException("Action name cannot be null or whitespace.", nameof(actionName));
        }

        _handlers[actionName] = handler ?? throw new ArgumentNullException(nameof(handler));
    }

    /// <summary>
    /// Validates whether an action can be executed
    /// </summary>
    /// <param name="state">The current game state</param>
    /// <param name="action">The action to validate</param>
    /// <returns>True if the action is valid, false otherwise</returns>
    public bool ValidateAction(GameState state, GameAction action)
    {
        if (state == null || action == null)
        {
            return false;
        }

        return _handlers.ContainsKey(action.ActionName);
    }

    /// <summary>
    /// Executes a game action
    /// </summary>
    /// <param name="state">The current game state</param>
    /// <param name="action">The action to execute</param>
    /// <returns>The result of executing the action</returns>
    public ActionResult ExecuteAction(GameState state, GameAction action)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(action);

        if (!_handlers.TryGetValue(action.ActionName, out var handler))
        {
            return ActionResult.Fail($"Unknown action: {action.ActionName}");
        }

        ActionResult? result = null;
        handler(state, action, r => result = r);
        return result ?? ActionResult.Fail("Action handler did not produce a result");
    }

    /// <summary>
    /// Registers the default built-in action handlers
    /// </summary>
    private void RegisterDefaultHandlers()
    {
        RegisterActionHandler("test_action", (state, action, onComplete) =>
        {
            var events = new List<GameEvent>
            {
                new GameEvent
                {
                    EventType = "TestAction",
                    PlayerId = state.PlayerId.ToString(),
                    Timestamp = DateTime.UtcNow,
                    Data = $"Test action executed with target: {action.TargetId}"
                }
            };
            onComplete(ActionResult.Ok("Test action successful", events));
        });

        RegisterActionHandler("param_action", (state, action, onComplete) =>
        {
            var events = new List<GameEvent>
            {
                new GameEvent
                {
                    EventType = "ParamAction",
                    PlayerId = state.PlayerId.ToString(),
                    Timestamp = DateTime.UtcNow,
                    Data = $"Param action executed with {action.Parameters?.Count ?? 0} parameters"
                }
            };
            onComplete(ActionResult.Ok("Param action successful", events));
        });
    }
}
