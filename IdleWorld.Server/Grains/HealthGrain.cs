using RealmsOfIdle.Core.Domain.Models;

namespace IdleWorld.Server.Grains;

internal class HealthGrain : Grain, IHealthGrain
{
    private readonly ILogger<HealthGrain> _logger;
    private readonly IGameService _gameService;

    public HealthGrain(ILogger<HealthGrain> logger, IGameService gameService)
    {
        _logger = logger;
        _gameService = gameService;
    }

    public async Task<GameHealth> GetHealthStatusAsync()
    {
        try
        {
            _logger.LogInformation("Health check requested for grain {GrainId}", this.GetGrainId());

            // Check game service health
            var gameServiceHealthy = await _gameService.IsHealthyAsync();

            // Check silo status
            var siloStatus = this.GetRuntime().GrainDirectory.GetGrainCount().ToString();

            // Determine overall health status
            var status = gameServiceHealthy ? HealthStatus.Healthy : HealthStatus.Degraded;

            var health = new GameHealth(
                status: status,
                mode: GameMode.Normal, // TODO: Get actual game mode from configuration
                timestamp: DateTime.UtcNow,
                siloStatus: siloStatus,
                dependencies: new Dictionary<string, string>
                {
                    ["GameService"] = gameServiceHealthy ? "Healthy" : "Unhealthy"
                });

            _logger.LogInformation("Health status check completed: {Status}", status);
            return health;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Health check failed for grain {GrainId}", this.GetGrainId());

            return new GameHealth(
                status: HealthStatus.Unhealthy,
                mode: GameMode.Normal,
                timestamp: DateTime.UtcNow,
                siloStatus: "Unknown",
                dependencies: new Dictionary<string, string>
                {
                    ["Error"] = ex.Message
                });
        }
    }
}
