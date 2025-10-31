using Microsoft.EntityFrameworkCore;
using RemixAspireStarter.Data.Models;

namespace RemixAspireStarter.Data
{
  public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : DbContext(options)
  {
    public DbSet<Widget> Widgets => Set<Widget>();
  }
}
