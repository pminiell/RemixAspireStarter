using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RemixAspireStarter.Data;

namespace RemixAspireStarter.API.Features;

public partial class Widgets
{

  public static class UpdateWidget
  {
    public record Command(int Id, string Name, int Quantity);

    public record Response(int Id, string Name, int Quantity);

    public class Handler
    {
      private readonly ApplicationDbContext _db;

      public Handler(ApplicationDbContext db)
      {
        _db = db;
      }

      public async Task<Response> HandleAsync(Command command, CancellationToken cancellationToken)
      {
        var widget = await _db.Widgets.FindAsync(new object[] { command.Id }, cancellationToken);
        if (widget != null)
        {
          widget.Name = command.Name;
          widget.Quantity = command.Quantity;

          await _db.SaveChangesAsync(cancellationToken);

          return new Response(widget.Id, widget.Name, widget.Quantity);
        }

        throw new KeyNotFoundException($"Widget with ID {command.Id} not found.");
      }
    }
  }

  public static IServiceCollection AddUpdateWidget(this IServiceCollection services)
  {
    return services.AddScoped<UpdateWidget.Handler>();
  }

  public static IEndpointRouteBuilder MapUpdateWidget(this IEndpointRouteBuilder endpoints)
  {
    endpoints.MapPut("/update", async ([FromBody] UpdateWidget.Command command, [FromServices] UpdateWidget.Handler handler, CancellationToken cancellationToken) =>
    {
      var result = await handler.HandleAsync(command, cancellationToken);
      return Results.Ok(result);
    });
    return endpoints;
  }
}

