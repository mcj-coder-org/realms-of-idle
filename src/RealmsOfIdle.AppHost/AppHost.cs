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

// Add API Server
var api = builder.AddProject<Projects.RealmsOfIdle_Server_Api>("api")
    .WithReference(postgresDb)
    .WithReference(redis)
    .WithExternalHttpEndpoints()
    .WaitFor(postgres)
    .WaitFor(redis);

builder.Build().Run();
