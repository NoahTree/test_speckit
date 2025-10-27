var builder = DistributedApplication.CreateBuilder(args);

// Add PostgreSQL server and database
var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin();

var taskifydb = postgres.AddDatabase("taskifydb");

// Add API Service
var apiService = builder.AddProject<Projects.Taskify_ApiService>("apiservice")
    .WithReference(taskifydb);

// Add Web Frontend
builder.AddProject<Projects.Taskify_Web>("webfrontend")
    .WithReference(apiService)
    .WithExternalHttpEndpoints();

builder.Build().Run();
