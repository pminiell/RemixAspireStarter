using Projects;

var builder = DistributedApplication.CreateBuilder(args);

//change db name to application appropriate name
var database = builder.AddPostgres("postgres").WithDataVolume().AddDatabase("AppDb", "table");

var worker = builder.AddProject<RemixAspireStarter_Worker>("remixaspirestarter-worker")
    .WithReference(database)
    .WaitFor(database);

var api = builder.AddProject<RemixAspireStarter_API>("remixaspirestarter-api")
  .WithReference(database).WaitFor(database).WaitFor(worker);

builder.AddNpmApp("remixaspirestarter-remixclient", "../RemixAspireStarter.RemixClient/",
     "dev")
    .WithReference(api).WaitFor(api).WithHttpEndpoint(targetPort: 5173, env: "VITE_PORT").WithExternalHttpEndpoints();

builder.Build().Run();
