using RemixAspireStarter.API.Features;
using RemixAspireStarter.Data;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<ApplicationDbContext>("AppDb");
Console.WriteLine("Configured ApplicationDbContext with Npgsql.");
builder.AddFeatures();
Console.WriteLine("Added API features. This changed.");

var app = builder.Build();
app.MapFeatures();

Console.WriteLine("Mapped features.");
app.Run();

