using RemixAspireStarter.API.Features;
using RemixAspireStarter.API.Features.Widgets;
using RemixAspireStarter.Data;

var builder = WebApplication.CreateBuilder(args);
var mediatRLicense = builder.Configuration["MediatRLicense"];

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<ApplicationDbContext>("AppDb");
builder.Services.AddMediatR(cfg => { cfg.LicenseKey = mediatRLicense; cfg.RegisterServicesFromAssembly(typeof(Program).Assembly); });
builder.Services.AddAutoMapper(cfg => { cfg.LicenseKey = mediatRLicense; cfg.AddMaps(typeof(Program).Assembly); });


var app = builder.Build();
app.MapWidgetsEndpoints();

app.Run();

