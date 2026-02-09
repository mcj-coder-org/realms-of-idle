var builder = DistributedApplication.CreateBuilder(args);

// Load .env into environment variables (ports, endpoint config)
var repoRoot = Path.GetFullPath(Path.Combine(builder.AppHostDirectory, "..", ".."));
var envPath = Path.Combine(repoRoot, ".env");
if (File.Exists(envPath))
{
    DotNetEnv.Env.Load(envPath);
}

var apiPort = int.TryParse(Environment.GetEnvironmentVariable("API_HTTP_PORT"), out var ap) ? ap : 5214;
var orleansPort = int.TryParse(Environment.GetEnvironmentVariable("ORLEANS_HTTP_PORT"), out var op) ? op : 5001;
var blazorPort = int.TryParse(Environment.GetEnvironmentVariable("BLAZOR_HTTP_PORT"), out var bp) ? bp : 5004;

// Add PostgreSQL (Aspire containers have built-in health checks)
var postgres = builder.AddPostgres("postgres")
    .WithImageTag("16-alpine")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var postgresDb = postgres.AddDatabase("realmsOfIdle");

// Add Redis (Aspire containers have built-in health checks)
var redis = builder.AddRedis("redis")
    .WithImageTag("7-alpine")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

// Add Orleans cluster
// NOTE: Orleans projects use fixed ports (silo: 11111, gateway: 30000) as workaround
// for Aspire 13.1 + Orleans 10 dynamic port issue: https://github.com/dotnet/aspire/issues/6940
// NOTE: Explicit cluster ID "default" to match UseLocalhostClustering() in both projects
var orleans = builder.AddOrleans("orleans")
    .WithDevelopmentClustering()
    .WithClusterId("default")
    .WithMemoryGrainStorage("Default");

// Add Orleans Silo
var orleansSilo = builder.AddProject<Projects.RealmsOfIdle_Server_Orleans>("orleans-silo")
    .WithReference(orleans)
    .WithReference(postgresDb)
    .WithReference(redis)
    .WithHttpHealthCheck("/health");

// Add API Server
// NOTE: .WaitFor(orleansSilo) removed due to health check blocking issue (#35)
// API will retry Orleans connection on startup
builder.AddProject<Projects.RealmsOfIdle_Server_Api>("api")
    .WithReference(postgresDb)
    .WithReference(redis)
    .WithReference(orleans.AsClient())
    .WithHttpHealthCheck("/health")
    .WaitFor(orleansSilo);

// Add Blazor WASM client
builder.AddProject<Projects.RealmsOfIdle_Client_Blazor>("blazor-client");

builder.Build().Run();
