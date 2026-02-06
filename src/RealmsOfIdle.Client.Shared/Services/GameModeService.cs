using Microsoft.Extensions.DependencyInjection;
using RealmsOfIdle.Client.Shared.Logging;
using RealmsOfIdle.Core.Abstractions;
using RealmsOfIdle.Core.Domain.Models;

namespace RealmsOfIdle.Client.Shared.Services;

/// <summary>
/// Service for managing game mode (offline/online) and routing to appropriate implementations
/// </summary>
public class GameModeService : IGameModeService
{
    private GameMode _currentMode = GameMode.Offline;
    private readonly IServiceProvider _serviceProvider;

    public event EventHandler? ModeChanged;

    public GameMode CurrentMode => _currentMode;

    public GameModeService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<GameMode> GetModeAsync()
    {
        return Task.FromResult(_currentMode);
    }

    public Task SetModeAsync(GameMode mode)
    {
        if (_currentMode != mode)
        {
            _currentMode = mode;
            OnModeChanged();
        }
        return Task.CompletedTask;
    }

    public Task<bool> IsOnlineAsync()
    {
        return Task.FromResult(_currentMode == GameMode.Online);
    }

    public Task ToggleModeAsync()
    {
        var newMode = _currentMode == GameMode.Offline ? GameMode.Online : GameMode.Offline;
        return SetModeAsync(newMode);
    }

    public IGameService GetGameService()
    {
        return _currentMode == GameMode.Offline
            ? _serviceProvider.GetRequiredService<LocalGameService>()
            : _serviceProvider.GetRequiredService<MultiplayerGameService>();
    }

    public IGameLogger GetLogger()
    {
        return _currentMode == GameMode.Offline
            ? _serviceProvider.GetRequiredService<LiteDBGameLogger>()
            : _serviceProvider.GetRequiredService<RemoteGameLogger>();
    }

    private void OnModeChanged()
    {
        ModeChanged?.Invoke(this, EventArgs.Empty);
    }
}

/// <summary>
/// Game mode service interface
/// </summary>
public interface IGameModeService
{
    /// <summary>
    /// Current game mode
    /// </summary>
    GameMode CurrentMode { get; }

    /// <summary>
    /// Event raised when mode changes
    /// </summary>
    event EventHandler? ModeChanged;

    /// <summary>
    /// Get current mode
    /// </summary>
    Task<GameMode> GetModeAsync();

    /// <summary>
    /// Set game mode
    /// </summary>
    Task SetModeAsync(GameMode mode);

    /// <summary>
    /// Check if currently in online mode
    /// </summary>
    Task<bool> IsOnlineAsync();

    /// <summary>
    /// Toggle between offline and online
    /// </summary>
    Task ToggleModeAsync();

    /// <summary>
    /// Get appropriate game service for current mode
    /// </summary>
    IGameService GetGameService();

    /// <summary>
    /// Get appropriate logger for current mode
    /// </summary>
    IGameLogger GetLogger();
}
