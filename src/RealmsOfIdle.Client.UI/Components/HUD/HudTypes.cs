namespace RealmsOfIdle.Client.UI.Components.HUD;

/// <summary>
/// Quick action slot for the action bar
/// </summary>
public record QuickActionSlot
{
    public required string Id { get; init; }
    public required string Label { get; init; }
    public required string Icon { get; init; }
    public string? KeyHint { get; init; }
    public bool IsActive { get; init; }
    public int? CooldownPercent { get; init; }

    public QuickActionSlot WithActive(bool isActive) => this with { IsActive = isActive };
    public QuickActionSlot WithCooldown(int? percent) => this with { CooldownPercent = percent };
}

/// <summary>
/// Player statistics for HUD display
/// </summary>
public record PlayerStats
{
    public int Gold { get; init; }
    public int PlayerLevel { get; init; }
    public int InnLevel { get; init; }
    public int Health { get; init; }
    public int MaxHealth { get; init; }
    public int Experience { get; init; }
    public int ExperienceToLevel { get; init; }
}

/// <summary>
/// Information panel content for object/character inspection
/// </summary>
public record InfoPanelContent
{
    public required string Title { get; init; }
    public string? Subtitle { get; init; }
    public required string Icon { get; init; }
    public Dictionary<string, string> Stats { get; init; } = new();
    public IReadOnlyList<InfoPanelAction> Actions { get; init; } = Array.Empty<InfoPanelAction>();
}

/// <summary>
/// Action button for info panel
/// </summary>
public record InfoPanelAction
{
    public required string Id { get; init; }
    public required string Label { get; init; }
    public string? Icon { get; init; }
}

/// <summary>
/// Control hint for help panel
/// </summary>
public record ControlHint
{
    public required string Key { get; init; }
    public required string Action { get; init; }
}
