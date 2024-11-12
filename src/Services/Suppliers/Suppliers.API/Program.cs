using HealthChecks.UI.Client;
using Suppliers.API;
using Suppliers.API.Data.Extensions;
using BuildingBlocks.Messaging.MassTransit;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddHealthChecks();
builder.Services.AddInfrastructureServices(builder.Configuration, builder.Environment);
builder.Services.AddServices();
builder.Services.AddMessageBroker(builder.Configuration, builder.Environment.IsDevelopment(), Assembly.GetExecutingAssembly());

var app = builder.Build();

app.MapCarter();


app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapGet("/", () => "Suppliers API");

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();

// package manager console commands to update and deploy the database:
// add-migration AddBookingReference -OutputDir Data/Migrations -StartupProject Suppliers.API
// update-database -StartupProject Suppliers.API
