namespace RealmsOfIdle.Client.Blazor;

// CA1515: Blazor components require public services for DI injection
#pragma warning disable CA1515

/// <summary>
/// HTTP-based game service for Blazor WASM client
/// Communicates with the Web API for all game operations
/// </summary>
public class HttpGameService
{
    private readonly HttpClient _httpClient;
    private readonly Microsoft.Extensions.Logging.ILogger<HttpGameService> _logger;

    public HttpGameService(HttpClient httpClient, Microsoft.Extensions.Logging.ILogger<HttpGameService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    /// <summary>
    /// Get game state from API
    /// </summary>
    public async Task<string?> GetGameStateAsync(string playerId, System.Threading.CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Fetching game state for player: {PlayerId}", playerId);
            var response = await _httpClient.GetAsync(new System.Uri($"/api/game/{playerId}", System.UriKind.Relative), cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var state = await response.Content.ReadAsStringAsync(cancellationToken);
                return state;
            }

            _logger.LogWarning("Failed to fetch game state: {StatusCode}", response.StatusCode);
            return null;
        }
        catch (System.IO.IOException ex)
        {
            _logger.LogError(ex, "Network error fetching game state");
            return null;
        }
        catch (System.Net.Http.HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP error fetching game state");
            return null;
        }
    }

    /// <summary>
    /// Perform game action via API
    /// </summary>
    public async Task<bool> PerformActionAsync(string playerId, string action, System.Threading.CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Performing action {Action} for player: {PlayerId}", action, playerId);
            var response = await _httpClient.PostAsync(new System.Uri($"/api/game/{playerId}/actions/{action}", System.UriKind.Relative), null, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Action {Action} completed successfully", action);
                return true;
            }

            _logger.LogWarning("Action {Action} failed: {StatusCode}", action, response.StatusCode);
            return false;
        }
        catch (System.IO.IOException ex)
        {
            _logger.LogError(ex, "Network error performing action");
            return false;
        }
        catch (System.Net.Http.HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP error performing action");
            return false;
        }
    }

    /// <summary>
    /// Check API health
    /// </summary>
    public async Task<bool> CheckApiHealthAsync(System.Threading.CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync(new System.Uri("/health", System.UriKind.Relative), cancellationToken);
            return response.IsSuccessStatusCode;
        }
        catch (System.IO.IOException)
        {
            return false;
        }
        catch (System.Net.Http.HttpRequestException)
        {
            return false;
        }
    }
}
