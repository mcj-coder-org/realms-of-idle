using RealmsOfIdle.Core.Domain.Models;

namespace RealmsOfIdle.Server.Api.Endpoints;

internal static class HealthEndpoints
{
    public static void MapHealthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/health", () =>
        {
            return Results.Ok(new GameHealth(
                Status: HealthStatus.Healthy,
                Mode: GameMode.Offline,
                Timestamp: DateTime.UtcNow
            ));
        })
        .WithName("GetHealth");
    }
}
