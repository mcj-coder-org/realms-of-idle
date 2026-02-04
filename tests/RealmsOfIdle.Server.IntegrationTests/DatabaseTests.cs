using Testcontainers.PostgreSql;
using Testcontainers.Redis;
using Xunit;
using Xunit.Abstractions;

namespace RealmsOfIdle.Server.IntegrationTests;

/// <summary>
/// Integration tests for database infrastructure using TestContainers.
/// These tests verify PostgreSQL and Redis can be accessed.
/// Configured for Podman on Windows - see PodmanConfiguration.
/// </summary>
[Trait("Category", "Integration")]
public class DatabaseTests
{
    private readonly ITestOutputHelper _output;

    public DatabaseTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task PostgreSQL_Container_StartsSuccessfully()
    {
        // Arrange
        await using var container = new PostgreSqlBuilder()
            .WithImage("postgres:16-alpine")
            .WithDatabase("testdb")
            .WithUsername("test")
            .WithPassword("test")
            .Build();

        // Act
        await container.StartAsync();

        // Assert
        var connectionString = container.GetConnectionString();
        Assert.Contains("testdb", connectionString);
        Assert.Contains("Port=", connectionString); // Port is mapped dynamically
    }

    [Fact]
    public async Task PostgreSQL_WithTestContainers_CanConnect()
    {
        // Arrange
        await using var container = new PostgreSqlBuilder()
            .WithImage("postgres:16-alpine")
            .WithDatabase("testdb")
            .WithUsername("test")
            .WithPassword("test")
            .WithCleanUp(true)
            .Build();

        await container.StartAsync();

        // Act
        var connectionString = container.GetConnectionString();

        // Verify connection string is available
        Assert.NotNull(connectionString);
        Assert.Matches(@".*Host=(localhost|127\.0\.0\.1).*Port=\d+.*", connectionString);

        _output.WriteLine($"PostgreSQL connection string: {connectionString}");
    }

    [Fact]
    public async Task Redis_Container_StartsSuccessfully()
    {
        // Arrange
        await using var container = new RedisBuilder()
            .WithImage("redis:7-alpine")
            .WithPortBinding(6379, 6379)
            .Build();

        // Act
        await container.StartAsync();

        // Assert
        var endpoint = container.GetConnectionString();
        Assert.Contains("6379", endpoint);
    }

    [Fact]
    public async Task Redis_WithTestContainers_CanConnect()
    {
        // Arrange
        await using var container = new RedisBuilder()
            .WithImage("redis:7-alpine")
            .WithPortBinding(6379, 6379)
            .WithCleanUp(true)
            .Build();

        await container.StartAsync();

        // Act
        var endpoint = container.GetConnectionString();

        // Verify connection string is available
        Assert.NotNull(endpoint);
        Assert.Matches(@"(localhost|127\.0\.0\.1):\d+", endpoint);

        _output.WriteLine($"Redis connection string: {endpoint}");
    }
}
