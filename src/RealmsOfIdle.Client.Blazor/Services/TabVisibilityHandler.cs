using Microsoft.JSInterop;

namespace RealmsOfIdle.Client.Blazor.Services;

/// <summary>
/// Handles tab visibility changes via JavaScript Page Visibility API interop.
/// Detects when the user switches tabs or minimizes the browser.
/// </summary>
public class TabVisibilityHandler : IAsyncDisposable
{
    private IJSObjectReference? _module;
    private DotNetObjectReference<TabVisibilityHandler>? _dotNetRef;

    /// <summary>
    /// Fired when the tab becomes hidden (user switches away)
    /// </summary>
    public event Action? OnTabHidden;

    /// <summary>
    /// Fired when the tab becomes visible again, with elapsed time since hidden
    /// </summary>
    public event Action<TimeSpan>? OnTabVisible;

    /// <summary>
    /// Initialize the JavaScript interop for Page Visibility API
    /// </summary>
    public async Task InitializeAsync(IJSRuntime jsRuntime)
    {
        _dotNetRef = DotNetObjectReference.Create(this);
        _module = await jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/tab-visibility.js");
        await _module.InvokeVoidAsync("initialize", _dotNetRef);
    }

    /// <summary>
    /// Called from JavaScript when tab becomes hidden
    /// </summary>
    [JSInvokable]
    public void NotifyTabHidden()
    {
        OnTabHidden?.Invoke();
    }

    /// <summary>
    /// Called from JavaScript when tab becomes visible, with elapsed seconds
    /// </summary>
    [JSInvokable]
    public void NotifyTabVisible(double elapsedSeconds)
    {
        OnTabVisible?.Invoke(TimeSpan.FromSeconds(elapsedSeconds));
    }

    public async ValueTask DisposeAsync()
    {
        if (_module != null)
        {
            await _module.InvokeVoidAsync("dispose");
            await _module.DisposeAsync();
        }

        _dotNetRef?.Dispose();

        GC.SuppressFinalize(this);
    }
}
