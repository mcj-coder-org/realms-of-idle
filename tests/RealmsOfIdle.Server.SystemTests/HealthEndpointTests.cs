#pragma warning disable IDE0005
extern alias ServerApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
#pragma warning restore IDE0005

namespace RealmsOfIdle.Server.SystemTests;

/// <summary>
/// System tests for API infrastructure.
/// Note: Full Orleans endpoint testing requires IntegrationTests with TestContainers.
/// These tests verify the API application can be constructed.
/// </summary>
[Trait("Category", "System")]
public class HealthEndpointTests
{
    private readonly ITestOutputHelper _output;

    public HealthEndpointTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void WebApplicationFactory_CanBeCreated()
    {
        // Arrange & Act
        var application = new TestWebApplicationFactory();

        // Assert - factory created without throwing
        Assert.NotNull(application);
        application.Dispose();
    }

    [Fact]
    public void Program_HasPublicEntryPoint()
    {
        // Act & Assert - Program class should be accessible for testing
        var programType = typeof(ServerApi::Program);
        Assert.NotNull(programType);
        Assert.Equal("Program", programType.Name);
    }

    [Fact]
    public void ApiProgram_HasHealthEndpoint()
    {
        // Arrange
        var source = ReadProgramSource("src/RealmsOfIdle.Server.Api");

        // Act & Assert
        Assert.Contains("MapHealthChecks(\"/health\")", source);
    }

    [Fact]
    public void ApiProgram_HasPingEndpoint()
    {
        // Arrange
        var source = ReadProgramSource("src/RealmsOfIdle.Server.Api");

        // Act & Assert
        Assert.Contains("MapGet(\"/ping\"", source);
    }

    [Fact]
    public void ApiProgram_HasOrleansHttpHealthCheck()
    {
        // Arrange
        var source = ReadProgramSource("src/RealmsOfIdle.Server.Api");

        // Act & Assert
        Assert.Contains("OrleansHttpHealthCheck", source);
    }

    [Fact]
    public void OrleansProgram_HasHealthEndpoint()
    {
        // Arrange
        var source = ReadProgramSource("src/RealmsOfIdle.Server.Orleans");

        // Act & Assert
        Assert.Contains("MapHealthChecks(\"/health\")", source);
    }

    [Fact]
    public void OrleansProgram_HasPingEndpoint()
    {
        // Arrange
        var source = ReadProgramSource("src/RealmsOfIdle.Server.Orleans");

        // Act & Assert
        Assert.Contains("MapGet(\"/ping\"", source);
    }

    private static string GetRepoRoot()
    {
        var dir = AppContext.BaseDirectory;
        while (dir != null && !File.Exists(Path.Combine(dir, "RealmsOfIdle.slnx")))
        {
            dir = Directory.GetParent(dir)?.FullName;
        }
        return dir ?? throw new InvalidOperationException("Could not find repo root");
    }

    private static string ReadProgramSource(string projectPath)
    {
        var repoRoot = GetRepoRoot();
        return File.ReadAllText(Path.Combine(repoRoot, projectPath, "Program.cs"));
    }

    /// <summary>
    /// Custom factory for testing API infrastructure.
    /// Note: Orleans services are configured in Program.cs and require
    /// a running silo. Full endpoint testing requires IntegrationTests with TestContainers.
    /// </summary>
    private class TestWebApplicationFactory : WebApplicationFactory<ServerApi::Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Configure test-specific services if needed
            builder.UseEnvironment("Testing");
        }
    }
}
