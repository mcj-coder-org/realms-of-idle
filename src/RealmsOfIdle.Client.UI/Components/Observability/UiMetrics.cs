namespace RealmsOfIdle.Client.UI.Components.Observability;

using System.Diagnostics.Metrics;

/// <summary>
/// UI metrics tracker using System.Diagnostics.Metrics
/// </summary>
public sealed class UiMetrics : IDisposable
{
    private static readonly Meter _sharedMeter = new("RealmsOfIdle.UI");
    private readonly Counter<long> _elementClicks;
    private readonly Counter<long> _panelViews;
    private readonly Counter<long> _themeChanges;
    private readonly Counter<long> _cameraPans;
    private readonly Counter<long> _quickActions;

    static UiMetrics()
    {
        // Initialize counters on the shared meter
    }

    public UiMetrics()
    {
        _elementClicks = _sharedMeter.CreateCounter<long>(
            "ui.element.clicks",
            description: "Number of UI element clicks");

        _panelViews = _sharedMeter.CreateCounter<long>(
            "ui.panel.views",
            description: "Number of panel views/opens");

        _themeChanges = _sharedMeter.CreateCounter<long>(
            "ui.theme.changes",
            description: "Number of theme changes");

        _cameraPans = _sharedMeter.CreateCounter<long>(
            "ui.camera.pans",
            description: "Number of camera pan operations");

        _quickActions = _sharedMeter.CreateCounter<long>(
            "ui.quick_actions",
            description: "Number of quick action activations");
    }

    public void Dispose()
    {
        // Don't dispose the shared meter as it's used across instances
    }

    /// <summary>
    /// Record a UI element click
    /// </summary>
    public void RecordElementClick(string elementType, string? elementId = null)
    {
        var tags = new KeyValuePair<string, object?>[]
        {
            new("element_type", elementType),
            new("element_id", elementId ?? string.Empty)
        };
        _elementClicks.Add(1, tags);
    }

    /// <summary>
    /// Record a panel view
    /// </summary>
    public void RecordPanelView(string panelType)
    {
        var tags = new KeyValuePair<string, object?>[]
        {
            new("panel_type", panelType)
        };
        _panelViews.Add(1, tags);
    }

    /// <summary>
    /// Record a theme change
    /// </summary>
    public void RecordThemeChange(string fromTheme, string toTheme)
    {
        var tags = new KeyValuePair<string, object?>[]
        {
            new("from_theme", fromTheme),
            new("to_theme", toTheme)
        };
        _themeChanges.Add(1, tags);
    }

    /// <summary>
    /// Record a camera pan operation
    /// </summary>
    public void RecordCameraPan(string direction, string method)
    {
        var tags = new KeyValuePair<string, object?>[]
        {
            new("direction", direction),
            new("method", method) // "drag", "keyboard", "button"
        };
        _cameraPans.Add(1, tags);
    }

    /// <summary>
    /// Record a quick action activation
    /// </summary>
    public void RecordQuickAction(string actionId)
    {
        var tags = new KeyValuePair<string, object?>[]
        {
            new("action_id", actionId)
        };
        _quickActions.Add(1, tags);
    }
}
