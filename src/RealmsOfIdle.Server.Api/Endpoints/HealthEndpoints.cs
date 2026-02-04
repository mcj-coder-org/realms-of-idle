using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RealmsOfIdle.Core;

namespace RealmsOfIdle.Server.Api.Endpoints;

public static class HealthEndpoints
{
    public static void MapHealthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/health", () =>
        {
            return Results.Ok(new GameHealth
            {
                Status = HealthStatus.Healthy,
                Timestamp = DateTime.UtcNow
            });
        })
        .WithName("GetHealth");
    }
}
