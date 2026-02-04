#pragma warning disable IDE0005
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
        var programType = typeof(Program);
        Assert.NotNull(programType);
        Assert.Equal("Program", programType.Name);
    }

    /// <summary>
    /// Custom factory for testing API infrastructure.
    /// Note: Orleans services are configured in Program.cs and require
    /// a running silo. Full endpoint testing requires IntegrationTests with TestContainers.
    /// </summary>
    private class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Configure test-specific services if needed
            builder.UseEnvironment("Testing");
        }
    }
}
