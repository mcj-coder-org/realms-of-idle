using RealmsOfIdle.Client.Shared.Services;
using RealmsOfIdle.Core.Domain;
using RealmsOfIdle.Core.Scenarios.Inn;
using RealmsOfIdle.Core.Scenarios.Inn.Persistence;
using LiteDB;
using Microsoft.Extensions.Logging;
using Moq;

namespace RealmsOfIdle.Client.Shared.Tests.Services;

[Trait("Category", "Unit")]
public sealed class LocalGameServiceTests : IDisposable
{
    private readonly Mock<ILogger<LocalGameService>> _loggerMock;
    private readonly LocalGameService _service;
    private readonly LiteDatabase _database;

    public LocalGameServiceTests()
    {
        _loggerMock = new Mock<ILogger<LocalGameService>>();
        // Use in-memory database for testing
        _database = new LiteDatabase(":memory:");
        _service = new LocalGameService(_database, _loggerMock.Object);
    }

    public void Dispose()
    {
        _database.Dispose();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task InitializeGameAsync_CreatesNewInnState()
    {
        // Arrange
        var playerId = "player123";
        var config = new GameConfiguration();

        // Act
        var session = await _service.InitializeGameAsync(playerId, config);

        // Assert
        Assert.NotNull(session);
        Assert.Equal(playerId, session.PlayerId);
        Assert.NotNull(session.InnState);
    }

    [Fact]
    public async Task InitializeGameAsync_SavesInnStateToDatabase()
    {
        // Arrange
        var playerId = "player123";
        var config = new GameConfiguration();

        // Act
        await _service.InitializeGameAsync(playerId, config);

        // Assert
        var innStates = _database.GetCollection<InnStateDto>("inn_states");
        var savedState = innStates.FindOne(x => x.PlayerId == playerId);
        Assert.NotNull(savedState);
        Assert.Equal(playerId, savedState.PlayerId);
    }

    [Fact]
    public async Task LoadGameAsync_RestoresInnState()
    {
        // Arrange
        var playerId = "player123";
        var config = new GameConfiguration();

        // Initialize and modify state
        var session = await _service.InitializeGameAsync(playerId, config);
        Assert.NotNull(session.InnState);
        var modifiedState = session.InnState.AddGold(100);
        session.InnState = modifiedState;
        await _service.SaveGameAsync(playerId);

        // Act
        var loadedSession = await _service.LoadGameAsync(playerId);

        // Assert
        Assert.NotNull(loadedSession);
        Assert.NotNull(loadedSession.InnState);
        Assert.Equal(100, loadedSession.InnState.Gold);
    }

    [Fact]
    public async Task ProcessTickAsync_UpdatesInnState()
    {
        // Arrange
        var playerId = "player123";
        var session = await _service.InitializeGameAsync(playerId, new GameConfiguration());

        // Act
        var updatedSession = await _service.ProcessTickAsync(playerId, TimeSpan.FromSeconds(1));

        // Assert
        Assert.NotNull(updatedSession);
        Assert.NotNull(updatedSession.InnState);
        Assert.True(updatedSession.CurrentTick >= 0);
    }

    [Fact]
    public async Task ProcessTickAsync_OfflineCatchUp_SimulatesMultipleTicks()
    {
        // Arrange
        var playerId = "player123";
        var session = await _service.InitializeGameAsync(playerId, new GameConfiguration());
        session.LastTickTime = DateTime.UtcNow.AddMinutes(-5);

        // Act
        var updatedSession = await _service.ProcessTickAsync(playerId, TimeSpan.FromSeconds(1));

        // Assert
        // Should catch up with some ticks (implementation caps at 1000)
        Assert.True(updatedSession.CurrentTick > 0);
    }

    [Fact]
    public async Task HandleActionAsync_ExecutesInnAction()
    {
        // Arrange
        var playerId = "player123";
        var session = await _service.InitializeGameAsync(playerId, new GameConfiguration());
        Assert.NotNull(session.InnState);

        // The initial state has 0 gold, so upgrading should fail
        var gameAction = new GameAction(InnActionTypes.UpgradeKitchen);

        // Act
        var result = await _service.HandleActionAsync(playerId, gameAction);

        // Assert - should fail because not enough gold
        Assert.False(result.Success);
        Assert.Contains("Not enough gold", result.Message);
    }
}
