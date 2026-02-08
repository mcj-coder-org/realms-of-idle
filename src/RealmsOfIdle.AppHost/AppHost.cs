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
// Blazor WebAssembly not compatible with .NET 10 - temporarily excluded
// var blazorPort = int.TryParse(Environment.GetEnvironmentVariable("BLAZOR_HTTP_PORT"), out var bp) ? bp : 5004;

// Add PostgreSQL with container health check
var postgres = builder.AddPostgres("postgres")
    .WithImageTag("16-alpine")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .WithHealthCheck("postgres");

var postgresDb = postgres.AddDatabase("realmsOfIdle");

// Add Redis with container health check
var redis = builder.AddRedis("redis")
    .WithImageTag("7-alpine")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .WithHealthCheck("redis");

// Add Orleans cluster
var orleans = builder.AddOrleans("orleans")
    .WithDevelopmentClustering()
    .WithMemoryGrainStorage("Default");

// Add Orleans Silo with HTTP health check
var orleansSilo = builder.AddProject<Projects.RealmsOfIdle_Server_Orleans>("orleans-silo")
    .WithReference(orleans)
    .WithReference(postgresDb)
    .WithReference(redis)
    .WithEndpoint("http", e => e.Port = orleansPort)
    .WithHttpHealthCheck("/health")
    .WaitFor(postgres)
    .WaitFor(redis);

// Add API Server with HTTP health check
builder.AddProject<Projects.RealmsOfIdle_Server_Api>("api")
    .WithReference(postgresDb)
    .WithReference(redis)
    .WithReference(orleans.AsClient())
    .WithEndpoint("http", e => e.Port = apiPort)
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WaitFor(orleansSilo)
    .WaitFor(postgres)
    .WaitFor(redis);

// Add Blazor WASM client
// Blazor WebAssembly not compatible with .NET 10 - temporarily excluded
// builder.AddProject<Projects.RealmsOfIdle_Client_Blazor>("blazor-client")
//     .WithEndpoint("http", e => e.Port = blazorPort)
//     .WithExternalHttpEndpoints();

builder.Build().Run();
