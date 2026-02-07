namespace RealmsOfIdle.Client.UI.Components.Theme;

/// <summary>
/// Available color themes for the UI
/// </summary>
public enum Theme
{
    /// <summary>Full color palette with multiple accent colors</summary>
    Color,

    /// <summary>Green monochrome theme (classic terminal)</summary>
    Green,

    /// <summary>Amber monochrome theme (classic terminal)</summary>
    Amber
}

/// <summary>
/// Theme change event arguments
/// </summary>
public record ThemeChangedEventArgs(Theme NewTheme);
