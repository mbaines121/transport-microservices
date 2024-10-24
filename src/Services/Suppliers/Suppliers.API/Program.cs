using HealthChecks.UI.Client;
using Suppliers.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddHealthChecks();
builder.Services.AddInfrastructureServices(builder.Configuration, builder.Environment);

var app = builder.Build();

app.MapCarter();

app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapGet("/", () => "Suppliers API");

app.Run();
