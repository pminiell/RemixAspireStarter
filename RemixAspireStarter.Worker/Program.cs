using Microsoft.EntityFrameworkCore;
using RemixAspireStarter.Data;
using RemixAspireStarter.Data.Models;
using RemixAspireStarter.Worker;

var builder = Host.CreateApplicationBuilder(args);

builder.AddNpgsqlDbContext<ApplicationDbContext>("AppDb", configureDbContextOptions: options =>
options.UseSeeding((context, _) =>
{
  if (!context.Set<Widget>().Any())
  {
    SeedWidgets(context);
    context.SaveChanges();
  }
}).UseAsyncSeeding(async (context, _, cancellationToken) =>
{
  if (!context.Set<Widget>().Any())
  {
    SeedWidgets(context);
    await context.SaveChangesAsync(cancellationToken);
  }
}));

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();

static void SeedWidgets(DbContext context)
{
  context.Set<Widget>().AddRange(
  [
    new Widget { Name = "Widget A", Quantity = 10 },
    new Widget { Name = "Widget B", Quantity = 20 },
    new Widget { Name = "Widget C", Quantity = 30 }
  ]);
}
;
