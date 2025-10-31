using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RemixAspireStarter.Data;
using RemixAspireStarter.Data.Models;
using Riok.Mapperly.Abstractions;

namespace RemixAspireStarter.API.Features;

public partial class Widgets
{
  public partial class CreateWidget
  {
    public record class Command(int Id, string Name, int Quantity);

    public record class Response(int Id, string Name, int Quantity);

    [Mapper]
    public partial class Mapper
    {
      public partial Widget Map(Command command);
      public partial Response Map(Widget widget);
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Quantity).GreaterThan(0);
      }
    }

    public class Handler
    {
      private readonly ApplicationDbContext _db;
      private readonly Mapper _mapper;

      public Handler(ApplicationDbContext db, Mapper mapper)
      {
        _db = db;
        _mapper = mapper;
      }
      public async Task<Response> HandleAsync(Command command, CancellationToken cancellationToken)
      {
        var widgetsExist = await _db.Widgets.AnyAsync(w => w.Id == command.Id, cancellationToken: cancellationToken);
        if (widgetsExist)
        {
          throw new Exception($"The widget already exists :{command.Id}");
        }
        Widget newWidget = _mapper.Map(command);
        _db.Widgets.Add(newWidget);
        await _db.SaveChangesAsync(cancellationToken);
        return _mapper.Map(newWidget);
      }
    }
  }
  public static IServiceCollection AddCreateWidget(this IServiceCollection services)
  {
    return services.AddScoped<CreateWidget.Handler>().AddSingleton<CreateWidget.Mapper>();
  }
  public static IEndpointRouteBuilder MapCreateWidget(this IEndpointRouteBuilder endpoints)
  {

    endpoints.MapPost("/create", async ([FromBody] CreateWidget.Command command, [FromServices] CreateWidget.Handler handler, CancellationToken cancellationToken) =>
        {
          var result = await handler.HandleAsync(command, cancellationToken);
          return TypedResults.Created($"/widgets/{result.Id}", result);
        });
    return endpoints;
  }
}
