using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var database = builder.AddPostgres("postgres").WithDataVolume().AddDatabase("AppDb", "table");

var worker = builder.AddProject<RemixAspireStarter_Worker>("remixaspirestarter-worker")
    .WithReference(database)
    .WaitFor(database);

builder.AddProject<RemixAspireStarter_API>("remixaspirestarter-api")
  .WithReference(database).WaitFor(database).WaitFor(worker);

builder.Build().Run();
