using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RemixAspireStarter.Data;
using RemixAspireStarter.Data.Models;

namespace RemixAspireStarter.API.Features;

public partial class Widgets
{
  public partial class DeleteWidget
  {
    public record class Command(int Id);

    public record class Response(int Id, DateTime DeletedAt);

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(x => x.Id).GreaterThan(0);
      }
    }

    public class Handler
    {
      private readonly ApplicationDbContext _db;

      public Handler(ApplicationDbContext db)
      {
        _db = db;
      }
      public async Task<Response> HandleAsync(Command command, CancellationToken cancellationToken)
      {
        var widgetsExist = await _db.Widgets.AnyAsync(w => w.Id == command.Id, cancellationToken: cancellationToken);
        if (!widgetsExist)
        {
          throw new Exception($"Cannot delete, the widget could not be found:{command.Id}");
        }
        Widget widgetToDelete = await _db.Widgets.FirstAsync(w => w.Id == command.Id, cancellationToken: cancellationToken);
        _db.Widgets.Remove(widgetToDelete);
        await _db.SaveChangesAsync(cancellationToken);
        return new Response(widgetToDelete.Id, DateTime.UtcNow);
      }
    }
  }
  public static IServiceCollection AddDeleteWidget(this IServiceCollection services)
  {
    return services.AddScoped<CreateWidget.Handler>();
  }
  public static IEndpointRouteBuilder MapDeleteWidget(this IEndpointRouteBuilder endpoints)
  {
    endpoints.MapDelete("/delete/", async ([FromBody] DeleteWidget.Command command, [FromServices] DeleteWidget.Handler handler, CancellationToken cancellationToken) =>
    {
      var result = await handler.HandleAsync(command, cancellationToken);
      return TypedResults.Ok(result);
    });
    return endpoints;
  }
}
