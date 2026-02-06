var builder = DistributedApplication.CreateBuilder(args);

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
builder.AddProject<Projects.RealmsOfIdle_Server_Orleans>("orleans-silo")
    .WithReference(orleans)
    .WaitFor(redis);

// Add API Server
builder.AddProject<Projects.RealmsOfIdle_Server_Api>("api")
    .WithReference(postgresDb)
    .WithReference(redis)
    .WithReference(orleans.AsClient())
    .WithExternalHttpEndpoints()
    .WaitFor(postgres)
    .WaitFor(redis);

builder.Build().Run();
