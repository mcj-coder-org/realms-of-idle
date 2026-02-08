using RealmsOfIdle.Core.Domain;
using RealmsOfIdle.Core.Engine;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Engine;

/// <summary>
/// Unit tests for ActionHandler
/// </summary>
[Trait("Category", "Unit")]
public class ActionHandlerTests
{
    [Fact]
    public void ExecuteAction_WithValidAction_ReturnsSuccessResult()
    {
        // Arrange
        var handler = new ActionHandler();
        var state = new TestGameState();
        var action = new GameAction("test_action");

        // Act
        var result = handler.ExecuteAction(state, action);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Message);
    }

    [Fact]
    public void ExecuteAction_WithNullAction_ThrowsArgumentNullException()
    {
        // Arrange
        var handler = new ActionHandler();
        var state = new TestGameState();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => handler.ExecuteAction(state, null!));
    }

    [Fact]
    public void ExecuteAction_WithNullState_ThrowsArgumentNullException()
    {
        // Arrange
        var handler = new ActionHandler();
        var action = new GameAction("test_action");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => handler.ExecuteAction(null!, action));
    }

    [Fact]
    public void ExecuteAction_WithUnknownAction_ReturnsFailureResult()
    {
        // Arrange
        var handler = new ActionHandler();
        var state = new TestGameState();
        var action = new GameAction("unknown_action");

        // Act
        var result = handler.ExecuteAction(state, action);

        // Assert
        Assert.False(result.Success);
        Assert.NotNull(result.Message);
    }

    [Fact]
    public void ExecuteAction_WithValidAction_GeneratesEvents()
    {
        // Arrange
        var handler = new ActionHandler();
        var state = new TestGameState();
        var action = new GameAction("test_action");

        // Act
        var result = handler.ExecuteAction(state, action);

        // Assert
        Assert.NotNull(result.Events);
        Assert.NotEmpty(result.Events);
    }

    [Fact]
    public void ExecuteAction_WithParameters_PassesToValidator()
    {
        // Arrange
        var handler = new ActionHandler();
        var state = new TestGameState();
        var parameters = new Dictionary<string, object>
        {
            { "amount", 100 },
            { "target", "enemy" }
        };
        var action = new GameAction("param_action", parameters: parameters);

        // Act
        var result = handler.ExecuteAction(state, action);

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public void RegisterActionHandler_WithCustomAction_UsesCustomHandler()
    {
        // Arrange
        var handler = new ActionHandler();
        var state = new TestGameState();
        var action = new GameAction("custom_action");
        var customCalled = false;

        void CustomHandler(GameState state, GameAction action, Action<ActionResult> onComplete)
        {
            customCalled = true;
            onComplete(ActionResult.Ok("Custom executed"));
        }

        handler.RegisterActionHandler("custom_action", CustomHandler);

        // Act
        var result = handler.ExecuteAction(state, action);

        // Assert
        Assert.True(customCalled);
        Assert.True(result.Success);
    }

    [Fact]
    public void ValidateAction_WithValidAction_ReturnsTrue()
    {
        // Arrange
        var handler = new ActionHandler();
        var state = new TestGameState();
        var action = new GameAction("test_action");

        // Act
        var isValid = handler.ValidateAction(state, action);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void ValidateAction_WithInvalidAction_ReturnsFalse()
    {
        // Arrange
        var handler = new ActionHandler();
        var state = new TestGameState();
        var action = new GameAction("invalid_action");

        // Act
        var isValid = handler.ValidateAction(state, action);

        // Assert
        Assert.False(isValid);
    }

    /// <summary>
    /// Minimal test game state for ActionHandler tests
    /// </summary>
    private class TestGameState : GameState
    {
        public TestGameState()
        {
        }
    }
}
