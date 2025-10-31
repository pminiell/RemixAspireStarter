using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RemixAspireStarter.Data;

namespace RemixAspireStarter.API.Features;

public partial class Widgets
{
  public partial class FetchWidgets
  {
    public record class Response(int Id, string Name, int Quantity);

    public class Handler
    {
      private readonly ApplicationDbContext _db;

      public Handler(ApplicationDbContext db)
      {
        _db = db;
      }
      public async Task<IEnumerable<Response>> HandleAsync(CancellationToken cancellationToken)
      {
        var widgets = await _db.Widgets.ToListAsync(cancellationToken);
        return widgets.Select(w => new Response(w.Id, w.Name, w.Quantity));
      }
    }
  }
  public static IServiceCollection AddFetchWidgets(this IServiceCollection services)
  {
    return services.AddScoped<FetchWidgets.Handler>();
  }
  public static IEndpointRouteBuilder MapFetchWidgets(this IEndpointRouteBuilder endpoints)
  {
    endpoints.MapGet("/", async ([FromServices] FetchWidgets.Handler handler, CancellationToken cancellationToken) =>
        {
          var result = await handler.HandleAsync(cancellationToken);
          return TypedResults.Ok(result);
        });
    return endpoints;
  }
}
