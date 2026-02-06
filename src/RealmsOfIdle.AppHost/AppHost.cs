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

// Add PostgreSQL
var postgres = builder.AddPostgres("postgres")
    .WithImageTag("16-alpine")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var postgresDb = postgres.AddDatabase("realmsOfIdle");

// Add Redis
var redis = builder.AddRedis("redis")
    .WithImageTag("7-alpine")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

// Add Orleans cluster
var orleans = builder.AddOrleans("orleans")
    .WithDevelopmentClustering()
    .WithMemoryGrainStorage("Default");

// Add Orleans Silo
var orleansSilo = builder.AddProject<Projects.RealmsOfIdle_Server_Orleans>("orleans-silo")
    .WithReference(orleans)
    .WithReference(postgresDb)
    .WithReference(redis)
    .WithEndpoint("http", e => e.Port = orleansPort)
    .WithHealthCheck("/health")
    .WaitFor(postgres)
    .WaitFor(redis);

// Add API Server
builder.AddProject<Projects.RealmsOfIdle_Server_Api>("api")
    .WithReference(postgresDb)
    .WithReference(redis)
    .WithReference(orleans.AsClient())
    .WithEndpoint("http", e => e.Port = apiPort)
    .WithExternalHttpEndpoints()
    .WithHealthCheck("/health")
    .WaitFor(orleansSilo)
    .WaitFor(postgres)
    .WaitFor(redis);

builder.Build().Run();
