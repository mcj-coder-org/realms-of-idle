using RealmsOfIdle.Client.Blazor.Services;

namespace RealmsOfIdle.Client.Blazor.Tests.Services;

/// <summary>
/// Tests for TabVisibilityHandler event firing
/// Note: JS interop is mocked - these test the C# event mechanics
/// </summary>
[Trait("Category", "Unit")]
public class TabVisibilityHandlerTests : IAsyncLifetime, IDisposable
{
    private readonly TabVisibilityHandler _handler = new();

    public void Dispose()
    {
        _handler.DisposeAsync().AsTask().GetAwaiter().GetResult();
        GC.SuppressFinalize(this);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync() => await _handler.DisposeAsync();

    // T137: OnTabHidden event fires correctly
    [Fact]
    public void OnTabHidden_WhenInvoked_FiresEvent()
    {
        var eventFired = false;

        _handler.OnTabHidden += () => eventFired = true;

        _handler.NotifyTabHidden();

        Assert.True(eventFired);
    }

    // T137: OnTabVisible event fires with elapsed time
    [Fact]
    public void OnTabVisible_WhenInvoked_FiresEventWithElapsedTime()
    {
        TimeSpan? receivedElapsed = null;

        _handler.OnTabVisible += elapsed => receivedElapsed = elapsed;

        _handler.NotifyTabVisible(300);

        Assert.NotNull(receivedElapsed);
        Assert.Equal(TimeSpan.FromSeconds(300), receivedElapsed.Value);
    }

    // T137: Multiple subscribers receive events
    [Fact]
    public void OnTabHidden_WithMultipleSubscribers_AllReceiveEvent()
    {
        var count = 0;

        _handler.OnTabHidden += () => count++;
        _handler.OnTabHidden += () => count++;

        _handler.NotifyTabHidden();

        Assert.Equal(2, count);
    }

    // T137: No error when no subscribers
    [Fact]
    public void NotifyTabHidden_WithNoSubscribers_DoesNotThrow()
    {
        // Fresh handler with no subscribers
        using var handler = new TabVisibilityHandlerForTest();

        var exception = Record.Exception(() => handler.NotifyTabHidden());

        Assert.Null(exception);
    }

    // T137: No error when no visible subscribers
    [Fact]
    public void NotifyTabVisible_WithNoSubscribers_DoesNotThrow()
    {
        using var handler = new TabVisibilityHandlerForTest();

        var exception = Record.Exception(() => handler.NotifyTabVisible(300));

        Assert.Null(exception);
    }

    /// <summary>
    /// Wrapper for tests that need a disposable handler without async
    /// </summary>
    private sealed class TabVisibilityHandlerForTest : IDisposable
    {
        private readonly TabVisibilityHandler _inner = new();

        public void NotifyTabHidden() => _inner.NotifyTabHidden();
        public void NotifyTabVisible(double elapsedSeconds) => _inner.NotifyTabVisible(elapsedSeconds);

        public void Dispose()
        {
            _inner.DisposeAsync().AsTask().GetAwaiter().GetResult();
        }
    }
}
