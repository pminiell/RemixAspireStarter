namespace RemixAspireStarter.API.Features;

public static partial class Widgets
{
  public static IServiceCollection AddWidgetsFeature(this IServiceCollection services)
  {
    services.AddFetchWidgets().AddCreateWidget().AddDeleteWidget().AddUpdateWidget();
    return services;
  }

  public static IEndpointRouteBuilder MapWidgetsFeature(this IEndpointRouteBuilder endpoints)
  {
    var group = endpoints.MapGroup(nameof(Widgets).ToLower()).WithTags(nameof(Widgets));

    group.MapFetchWidgets().MapCreateWidget().MapDeleteWidget().MapUpdateWidget();
    return endpoints;
  }
}
