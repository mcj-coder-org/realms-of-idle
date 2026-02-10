using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace RealmsOfIdle.Client.Blazor.Services;

/// <summary>
/// Handles browser tab visibility changes to trigger offline progress calculation.
/// Uses Page Visibility API: https://developer.mozilla.org/en-US/docs/Web/API/Page_Visibility_API
/// </summary>
public sealed class TabVisibilityHandler : IAsyncDisposable
{
    private readonly IJSRuntime _jsRuntime;
    private IJSObjectReference? _module;
    private DotNetObjectReference<TabVisibilityHandler>? _dotNetRef;
    private DateTime _tabHiddenTime;
    private bool _isHidden;

    public event Func<TimeSpan, Task>? OnTabVisible; // Fires when tab becomes visible with elapsed time
    public event Func<Task>? OnTabHidden; // Fires when tab becomes hidden

    public TabVisibilityHandler(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    /// <summary>
    /// Initialize visibility change listener. Call in OnAfterRenderAsync(firstRender: true).
    /// </summary>
    public async Task InitializeAsync()
    {
        _module = await _jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/tab-visibility.js");

        _dotNetRef = DotNetObjectReference.Create(this);

        await _module.InvokeVoidAsync("initialize", _dotNetRef);
    }

    /// <summary>
    /// Called from JavaScript when tab visibility changes.
    /// </summary>
    [JSInvokable]
    public async Task OnVisibilityChange(bool isHidden)
    {
        if (isHidden && !_isHidden)
        {
            // Tab just became hidden
            _isHidden = true;
            _tabHiddenTime = DateTime.UtcNow;

            if (OnTabHidden != null)
                await OnTabHidden.Invoke();
        }
        else if (!isHidden && _isHidden)
        {
            // Tab just became visible
            _isHidden = false;
            var elapsed = DateTime.UtcNow - _tabHiddenTime;

            if (OnTabVisible != null)
                await OnTabVisible.Invoke(elapsed);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_module != null)
        {
            await _module.InvokeVoidAsync("cleanup");
            await _module.DisposeAsync();
        }

        _dotNetRef?.Dispose();
    }
}
