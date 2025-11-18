using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace RemixAspireStarter.API.Features.Widgets;

public static class WidgetsEndpoints
{
  public static void MapWidgetsEndpoints(this IEndpointRouteBuilder app)
  {
    app.MapGet("/widgets", async (IMediator mediator, CancellationToken cancellationToken) =>
    {
      var query = new FetchWidgets.Query();
      var result = await mediator.Send(query, cancellationToken);
      return Results.Ok(result);
    })
    .WithName("FetchWidgets")
    .WithOpenApi(); // Optional: Adds OpenAPI metadata if using Swagger

    app.MapPost("/widgets", async (IMediator mediator, CreateWidget.Command command, CancellationToken cancellationToken) =>
    {
      var result = await mediator.Send(command, cancellationToken);
      return Results.Created($"/widgets/{result.Id}", result);
    })
    .WithName("CreateWidget")
    .WithOpenApi();

    app.MapPut("/widgets", async (IMediator mediator, UpdateWidget.Command command, CancellationToken cancellationToken) =>
    {
      var result = await mediator.Send(command, cancellationToken);
      return Results.Ok(result);
    })
    .WithName("UpdateWidget")
    .WithOpenApi();

    app.MapDelete("/widgets/{id:int}", async (IMediator mediator, int id, CancellationToken cancellationToken) =>
    {
      var command = new DeleteWidget.Command(id);
      var result = await mediator.Send(command, cancellationToken);
      return Results.Ok(result);
    })
    .WithName("DeleteWidget")
    .WithOpenApi();

  }
}
