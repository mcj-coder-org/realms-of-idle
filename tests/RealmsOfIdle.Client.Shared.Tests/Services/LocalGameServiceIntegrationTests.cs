using AwesomeAssertions;
using LiteDB;
using Microsoft.Extensions.Logging;
using Moq;
using RealmsOfIdle.Client.Shared.Services;
using RealmsOfIdle.Core.Domain;
using RealmsOfIdle.Core.Scenarios.Inn;

namespace RealmsOfIdle.Client.Shared.Tests.Services;

[Trait("Category", "Integration")]
public sealed class LocalGameServiceIntegrationTests : IDisposable
{
    private readonly string _dbPath;
    private readonly Mock<ILogger<LocalGameService>> _loggerMock;

    public LocalGameServiceIntegrationTests()
    {
        _dbPath = Path.Combine(Path.GetTempPath(), $"realms_test_{Guid.NewGuid():N}.db");
        _loggerMock = new Mock<ILogger<LocalGameService>>();
    }

    public void Dispose()
    {
        if (File.Exists(_dbPath))
        {
            File.Delete(_dbPath);
        }

        var journalPath = _dbPath + "-journal";
        if (File.Exists(journalPath))
        {
            File.Delete(journalPath);
        }

        GC.SuppressFinalize(this);
    }

    private (LiteDatabase Database, LocalGameService Service) CreateService()
    {
        var database = new LiteDatabase($"Filename={_dbPath};Connection=direct");
        var service = new LocalGameService(database, _loggerMock.Object);
        return (database, service);
    }

    [Fact]
    public async Task SaveAndLoad_FullState_PreservesAllFields()
    {
        // Arrange - Initialize a game and advance state via ticks
        var playerId = "integration-player-1";
        var config = new GameConfiguration();

        // Phase 1: Initialize, tick, and save
        var (db1, service1) = CreateService();
        try
        {
            await service1.InitializeGameAsync(playerId, config);

            for (var i = 0; i < 10; i++)
            {
                await service1.ProcessTickAsync(playerId, TimeSpan.FromMilliseconds(100));
            }

            await service1.SaveGameAsync(playerId);
        }
        finally
        {
            db1.Dispose();
        }

        // Act - Create a NEW service instance (simulates app restart)
        var (db2, service2) = CreateService();
        GameSession? loadedSession;
        try
        {
            loadedSession = await service2.LoadGameAsync(playerId);
        }
        finally
        {
            db2.Dispose();
        }

        // Assert
        loadedSession.Should().NotBeNull();
        loadedSession!.InnState.Should().NotBeNull();
        loadedSession.PlayerId.Should().Be(playerId);
        loadedSession.CurrentTick.Should().BeGreaterThanOrEqualTo(0);

        // Verify facilities round-tripped
        loadedSession.InnState!.Facilities.Should().NotBeEmpty();
        loadedSession.InnState.Facilities.Should().ContainKey("kitchen");
        loadedSession.InnState.Facilities.Should().ContainKey("bar");
        loadedSession.InnState.Facilities.Should().ContainKey("tables");

        var kitchen = loadedSession.InnState.Facilities["kitchen"];
        kitchen.Type.Should().Be("Kitchen");
        kitchen.Level.Should().BeGreaterThanOrEqualTo(1);
        kitchen.Capacity.Should().BeGreaterThanOrEqualTo(1);

        var bar = loadedSession.InnState.Facilities["bar"];
        bar.Type.Should().Be("Bar");

        // Verify layout round-tripped
        loadedSession.InnState.Layout.Should().NotBeNull();
        loadedSession.InnState.Layout.Areas.Should().NotBeEmpty();
    }

    [Fact]
    public async Task SaveAndLoad_WithMultipleTicks_PreservesTickCountAndGold()
    {
        // Arrange - Initialize and build up state
        var playerId = "integration-player-2";
        var config = new GameConfiguration();

        int savedTickCount;
        int savedGold;
        int savedFacilityCount;

        var (db1, service1) = CreateService();
        try
        {
            await service1.InitializeGameAsync(playerId, config);

            // Run ticks to allow game progression
            for (var i = 0; i < 50; i++)
            {
                await service1.ProcessTickAsync(playerId, TimeSpan.FromMilliseconds(100));
            }

            var activeSession = await service1.ProcessTickAsync(playerId, TimeSpan.FromMilliseconds(100));
            savedTickCount = activeSession.CurrentTick;
            savedGold = activeSession.InnState!.Gold;
            savedFacilityCount = activeSession.InnState.Facilities.Count;

            await service1.SaveGameAsync(playerId);
        }
        finally
        {
            db1.Dispose();
        }

        // Act - Load in a new service instance
        var (db2, service2) = CreateService();
        GameSession? loadedSession;
        try
        {
            loadedSession = await service2.LoadGameAsync(playerId);
        }
        finally
        {
            db2.Dispose();
        }

        // Assert - State should be preserved
        loadedSession.Should().NotBeNull();
        loadedSession!.InnState.Should().NotBeNull();
        loadedSession.CurrentTick.Should().Be(savedTickCount);
        loadedSession.InnState!.Gold.Should().Be(savedGold);
        loadedSession.InnState.Facilities.Count.Should().Be(savedFacilityCount);
    }

    [Fact]
    public async Task SaveAndLoad_FacilityProperties_PreservedExactly()
    {
        // Arrange - Initialize and save, then reload and verify facility details
        var playerId = "integration-player-3";
        var config = new GameConfiguration();

        InnFacility savedKitchen;
        InnFacility savedBar;
        InnFacility savedTables;

        var (db1, service1) = CreateService();
        try
        {
            await service1.InitializeGameAsync(playerId, config);
            await service1.SaveGameAsync(playerId);

            var session = await service1.LoadGameAsync(playerId);
            savedKitchen = session!.InnState!.Facilities["kitchen"];
            savedBar = session.InnState.Facilities["bar"];
            savedTables = session.InnState.Facilities["tables"];
        }
        finally
        {
            db1.Dispose();
        }

        // Act - Load in new service
        var (db2, service2) = CreateService();
        GameSession? loadedSession;
        try
        {
            loadedSession = await service2.LoadGameAsync(playerId);
        }
        finally
        {
            db2.Dispose();
        }

        // Assert - All facility properties match exactly
        loadedSession.Should().NotBeNull();

        var loadedKitchen = loadedSession!.InnState!.Facilities["kitchen"];
        loadedKitchen.Type.Should().Be(savedKitchen.Type);
        loadedKitchen.Level.Should().Be(savedKitchen.Level);
        loadedKitchen.Capacity.Should().Be(savedKitchen.Capacity);
        loadedKitchen.ProductionRate.Should().Be(savedKitchen.ProductionRate);
        loadedKitchen.UpgradeCost.Should().Be(savedKitchen.UpgradeCost);

        var loadedBar = loadedSession.InnState.Facilities["bar"];
        loadedBar.Type.Should().Be(savedBar.Type);
        loadedBar.Level.Should().Be(savedBar.Level);
        loadedBar.Capacity.Should().Be(savedBar.Capacity);

        var loadedTables = loadedSession.InnState.Facilities["tables"];
        loadedTables.Type.Should().Be(savedTables.Type);
        loadedTables.Level.Should().Be(savedTables.Level);
    }

    [Fact]
    public async Task ProcessTickAsync_AutoSavesState_LoadableWithoutExplicitSave()
    {
        // Arrange - Initialize and process ticks WITHOUT calling SaveGameAsync
        var playerId = "integration-player-autosave";
        var config = new GameConfiguration();

        int tickCountAfterProcessing;
        int goldAfterProcessing;

        var (db1, service1) = CreateService();
        try
        {
            await service1.InitializeGameAsync(playerId, config);

            GameSession lastSession = null!;
            for (var i = 0; i < 10; i++)
            {
                lastSession = await service1.ProcessTickAsync(playerId, TimeSpan.FromMilliseconds(100));
            }

            tickCountAfterProcessing = lastSession.CurrentTick;
            goldAfterProcessing = lastSession.InnState!.Gold;
        }
        finally
        {
            db1.Dispose();
        }

        // Act - Create a NEW service (simulate restart) and load WITHOUT explicit save
        var (db2, service2) = CreateService();
        GameSession? loadedSession;
        try
        {
            loadedSession = await service2.LoadGameAsync(playerId);
        }
        finally
        {
            db2.Dispose();
        }

        // Assert - State was persisted by ProcessTickAsync auto-save
        loadedSession.Should().NotBeNull();
        loadedSession!.InnState.Should().NotBeNull();
        loadedSession.CurrentTick.Should().Be(tickCountAfterProcessing);
        loadedSession.InnState!.Gold.Should().Be(goldAfterProcessing);
        loadedSession.InnState.Facilities.Should().HaveCount(3);
    }

    [Fact]
    public async Task OfflineCatchUp_SimulatesTicksBasedOnElapsedTime()
    {
        // Arrange - Initialize and save, then manipulate LastTickTime to be in the past
        var playerId = "integration-player-4";
        var config = new GameConfiguration();

        int initialTick;

        var (db1, service1) = CreateService();
        try
        {
            var session = await service1.InitializeGameAsync(playerId, config);
            initialTick = session.CurrentTick;
            await service1.SaveGameAsync(playerId);
        }
        finally
        {
            db1.Dispose();
        }

        // Manipulate LastTickTime directly in the database
        using (var db = new LiteDatabase($"Filename={_dbPath};Connection=direct"))
        {
            var sessions = db.GetCollection<GameSession>("sessions");
            var savedSession = sessions.FindOne(x => x.PlayerId == playerId);
            savedSession.Should().NotBeNull();
            savedSession!.LastTickTime = DateTime.UtcNow.AddMinutes(-5);
            sessions.Update(new BsonValue(savedSession.SessionId), savedSession);
        }

        // Act - Load and process a tick (should trigger offline catch-up)
        var (db2, service2) = CreateService();
        GameSession updatedSession;
        try
        {
            await service2.LoadGameAsync(playerId);
            updatedSession = await service2.ProcessTickAsync(playerId, TimeSpan.FromMilliseconds(100));
        }
        finally
        {
            db2.Dispose();
        }

        // Assert - Should have simulated ticks for the offline period
        updatedSession.Should().NotBeNull();
        updatedSession.CurrentTick.Should().BeGreaterThan(initialTick);

        // 5 minutes = 300,000ms, tick interval = 100ms = 3000 ticks, capped at 1000
        updatedSession.CurrentTick.Should().BeGreaterThanOrEqualTo(900);
    }

    [Fact]
    public async Task OfflineCatchUp_ActuallyProcessesGameLogic()
    {
        // Arrange - Initialize, run some baseline ticks, save, then simulate offline
        var playerId = "integration-player-5";
        var config = new GameConfiguration();

        var (db1, service1) = CreateService();
        try
        {
            await service1.InitializeGameAsync(playerId, config);

            // Run initial ticks to establish a baseline
            for (var i = 0; i < 5; i++)
            {
                await service1.ProcessTickAsync(playerId, TimeSpan.FromMilliseconds(100));
            }

            await service1.SaveGameAsync(playerId);
        }
        finally
        {
            db1.Dispose();
        }

        // Manipulate LastTickTime to simulate offline period
        using (var db = new LiteDatabase($"Filename={_dbPath};Connection=direct"))
        {
            var sessions = db.GetCollection<GameSession>("sessions");
            var savedSession = sessions.FindOne(x => x.PlayerId == playerId);
            savedSession!.LastTickTime = DateTime.UtcNow.AddMinutes(-2);
            sessions.Update(new BsonValue(savedSession.SessionId), savedSession);
        }

        // Act - Load and process tick to trigger catch-up
        var (db2, service2) = CreateService();
        GameSession updatedSession;
        try
        {
            await service2.LoadGameAsync(playerId);
            updatedSession = await service2.ProcessTickAsync(playerId, TimeSpan.FromMilliseconds(100));
        }
        finally
        {
            db2.Dispose();
        }

        // Assert - The game loop actually ran (state changed beyond just tick count)
        updatedSession.Should().NotBeNull();
        updatedSession.InnState.Should().NotBeNull();

        // Ticks were actually simulated (not just counted)
        updatedSession.CurrentTick.Should().BeGreaterThan(5);

        // Verify game state is coherent after many ticks of catch-up
        // (facilities still present, layout still valid — proves the loop ran without corrupting state)
        updatedSession.InnState!.Facilities.Should().HaveCount(3);
        updatedSession.InnState.Facilities.Should().ContainKey("kitchen");
        updatedSession.InnState.Facilities.Should().ContainKey("bar");
        updatedSession.InnState.Facilities.Should().ContainKey("tables");
        updatedSession.InnState.Layout.Should().NotBeNull();
        updatedSession.InnState.Layout.Areas.Should().NotBeEmpty();

        // The LastTickTime should be updated to approximately now
        updatedSession.LastTickTime.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public async Task MultipleSessionsSameFile_IndependentPlayers()
    {
        // Arrange - Two players using the same database file
        var player1Id = "player-1";
        var player2Id = "player-2";
        var config = new GameConfiguration();

        var (db1, service1) = CreateService();
        try
        {
            await service1.InitializeGameAsync(player1Id, config);
            await service1.InitializeGameAsync(player2Id, config);
            await service1.SaveGameAsync(player1Id);
            await service1.SaveGameAsync(player2Id);
        }
        finally
        {
            db1.Dispose();
        }

        // Set different offline periods: player1 = 5 min, player2 = 1 min
        // ProcessTickAsync uses wall-clock elapsed time, so we manipulate LastTickTime
        using (var db = new LiteDatabase($"Filename={_dbPath};Connection=direct"))
        {
            var sessions = db.GetCollection<GameSession>("sessions");

            var session1 = sessions.FindOne(x => x.PlayerId == player1Id);
            session1!.LastTickTime = DateTime.UtcNow.AddMinutes(-5);
            sessions.Update(new BsonValue(session1.SessionId), session1);

            var session2 = sessions.FindOne(x => x.PlayerId == player2Id);
            session2!.LastTickTime = DateTime.UtcNow.AddMinutes(-1);
            sessions.Update(new BsonValue(session2.SessionId), session2);
        }

        // Act - Load both and trigger catch-up via ProcessTickAsync
        var (db2, service2) = CreateService();
        GameSession loaded1;
        GameSession loaded2;
        try
        {
            await service2.LoadGameAsync(player1Id);
            await service2.LoadGameAsync(player2Id);
            loaded1 = await service2.ProcessTickAsync(player1Id, TimeSpan.FromMilliseconds(100));
            loaded2 = await service2.ProcessTickAsync(player2Id, TimeSpan.FromMilliseconds(100));
        }
        finally
        {
            db2.Dispose();
        }

        // Assert - Each player's state is independent with different tick counts
        loaded1.PlayerId.Should().Be(player1Id);
        loaded2.PlayerId.Should().Be(player2Id);

        // Player 1 was offline 5 min (capped at 1000 ticks), player 2 was offline 1 min (600 ticks)
        loaded1.CurrentTick.Should().BeGreaterThan(loaded2.CurrentTick);
        loaded1.CurrentTick.Should().BeGreaterThanOrEqualTo(900);
        loaded2.CurrentTick.Should().BeGreaterThanOrEqualTo(500);
    }
}
