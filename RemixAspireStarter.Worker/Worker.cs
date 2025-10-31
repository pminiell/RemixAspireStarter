using Microsoft.EntityFrameworkCore;
using RemixAspireStarter.Data;

namespace RemixAspireStarter.Worker;

public class Worker(IServiceProvider serviceProvider) : BackgroundService
{
  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    using var scope = serviceProvider.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    await dbContext.Database.MigrateAsync(stoppingToken);
  }
}
