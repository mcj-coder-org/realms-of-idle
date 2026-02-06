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

// Add Orleans Silo
var orleans = builder.AddProject<Projects.RealmsOfIdle_Server_Orleans>("orleans")
    .WithReference(redis)
    .WithExternalHttpEndpoints()
    .WaitFor(redis);

// Add API Server
var api = builder.AddProject<Projects.RealmsOfIdle_Server_Api>("api")
    .WithReference(postgresDb)
    .WithReference(redis)
    .WithReference(orleans)
    .WithExternalHttpEndpoints()
    .WaitFor(postgres)
    .WaitFor(redis)
    .WaitFor(orleans);

builder.Build().Run();
