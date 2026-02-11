using System.Net.Http.Json;
using RealmsOfIdle.Core.Abstractions;
using RealmsOfIdle.Core.Domain;

namespace RealmsOfIdle.Client.Shared.Services;

/// <summary>
/// HTTP-based game service for online play
/// </summary>
public class MultiplayerGameService : IGameService
{
    private readonly HttpClient _httpClient;
    private readonly Uri _baseUri;

    public MultiplayerGameService(HttpClient httpClient, Uri? baseUrl = null)
    {
        _httpClient = httpClient;
        _baseUri = baseUrl ?? new Uri("https://localhost:5001");
    }

    public async Task<GameSession> InitializeGameAsync(string playerId, GameConfiguration configuration)
    {
        var uri = new Uri(_baseUri, "/api/game/initialize");
        var response = await _httpClient.PostAsJsonAsync(uri, new
        {
            playerId,
            configuration
        });

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<GameSession>()
            ?? throw new InvalidOperationException("Failed to initialize game");
    }

    public async Task<GameSession?> GetActiveSessionAsync(string playerId)
    {
        var uri = new Uri(_baseUri, $"/api/game/session/{playerId}");
        var response = await _httpClient.GetAsync(uri);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<GameSession>();
    }

    public async Task<GameSession> ProcessTickAsync(string playerId, TimeSpan deltaTime)
    {
        var uri = new Uri(_baseUri, "/api/game/tick");
        var response = await _httpClient.PostAsJsonAsync(uri, new
        {
            playerId,
            deltaTimeMs = deltaTime.TotalMilliseconds
        });

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<GameSession>()
            ?? throw new InvalidOperationException("Failed to process tick");
    }

    public async Task<ActionResult> HandleActionAsync(string playerId, GameAction action)
    {
        var uri = new Uri(_baseUri, "/api/game/action");
        var response = await _httpClient.PostAsJsonAsync(uri, new
        {
            playerId,
            actionType = action.GetType().Name,
            action
        });

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ActionResult>()
            ?? ActionResult.Fail("Unknown error");
    }

    public async Task SaveGameAsync(string playerId)
    {
        var uri = new Uri(_baseUri, $"/api/game/save/{playerId}");
        var response = await _httpClient.PostAsync(uri, null);
        response.EnsureSuccessStatusCode();
    }

    public async Task<GameSession?> LoadGameAsync(string playerId)
    {
        return await GetActiveSessionAsync(playerId);
    }

    public async Task<PlayerProfile> GetOrCreatePlayerProfileAsync(string playerId)
    {
        var uri = new Uri(_baseUri, $"/api/player/{playerId}");
        var response = await _httpClient.GetAsync(uri);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<PlayerProfile>()
                ?? new PlayerProfile { PlayerId = playerId };
        }

        // Create profile if not exists
        var createUri = new Uri(_baseUri, "/api/player");
        var createResponse = await _httpClient.PostAsJsonAsync(createUri, new
        {
            playerId
        });

        createResponse.EnsureSuccessStatusCode();
        return await createResponse.Content.ReadFromJsonAsync<PlayerProfile>()
            ?? new PlayerProfile { PlayerId = playerId };
    }

    public async Task<GameStats> GetGameStatsAsync(string playerId)
    {
        var uri = new Uri(_baseUri, $"/api/player/{playerId}/stats");
        var response = await _httpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<GameStats>()
            ?? new GameStats { FirstPlayed = DateTime.UtcNow, LastPlayed = DateTime.UtcNow };
    }

    public Task<ActionResult> PossessNPCAsync(string npcId)
    {
        throw new NotImplementedException("Possession not supported in multiplayer mode");
    }

    public Task ReleaseNPCAsync(string npcId)
    {
        throw new NotImplementedException("Possession not supported in multiplayer mode");
    }

    public Task<ActionResult> ExecuteActionAsync(string npcId, string actionId)
    {
        throw new NotImplementedException("Possession not supported in multiplayer mode");
    }
}
