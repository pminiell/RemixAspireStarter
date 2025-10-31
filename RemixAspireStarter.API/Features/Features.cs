using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace RemixAspireStarter.API.Features
{
  public static class Features
  {
    public static IServiceCollection AddFeatures(this WebApplicationBuilder builder)
    {
      builder.AddFluentValidationEndpointFilter();
      return builder.Services.AddFluentValidationAutoValidation().AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()).AddWidgetsFeature().AddDbContext<Data.ApplicationDbContext>();
    }
    public static IEndpointRouteBuilder MapFeatures(this IEndpointRouteBuilder endpoints)
    {
      var group = endpoints.MapGroup("/").AddFluentValidationFilter();
      group.MapWidgetsFeature();
      return endpoints;
    }
  }
}
