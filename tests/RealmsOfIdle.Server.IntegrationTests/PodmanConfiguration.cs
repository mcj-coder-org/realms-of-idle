using System.Runtime.CompilerServices;

namespace RealmsOfIdle.Server.IntegrationTests;

/// <summary>
/// Configures TestContainers for different container runtimes.
///
/// IMPORTANT: Testcontainers for .NET on Windows requires Docker Desktop.
/// Podman on Windows with WSL backend is NOT directly supported because
/// the Podman socket is inside WSL, not accessible from Windows directly.
///
/// **To run IntegrationTests with Podman:**
/// Run the tests inside WSL using: `run-tests-wsl.bat` or `wsl bash run-tests-wsl.sh`
///
/// **To run IntegrationTests with Docker Desktop:**
/// Simply run: `dotnet test tests/RealmsOfIdle.Server.IntegrationTests`
/// </summary>
internal static class PodmanConfiguration
{
    [ModuleInitializer]
    public static void ConfigureContainerRuntime()
    {
        // Disable Ryuk for Podman (rootless mode doesn't support Ryuk's container management)
        // This is safe for Docker Desktop too, as Ryuk is optional
        Environment.SetEnvironmentVariable("TESTCONTAINERS_RYUK_DISABLED", "true");
    }
}
