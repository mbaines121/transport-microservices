using Carter;

namespace Suppliers.API.Endpoints;

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/connectionstring", (IConfiguration configuration) =>
        {
            var connectionString = configuration.GetConnectionString("Database");

            return Results.Ok(connectionString);
        })
        .WithName("ConnectionString")
        .Produces<string>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Connection String")
        .WithDescription("Get Connection String");

        app.MapGet("/environment", (IWebHostEnvironment environment) =>
        {
            return Results.Ok(environment.EnvironmentName);
        })
        .WithName("Environment")
        .Produces<string>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Environment")
        .WithDescription("Get Environment");
    }
}
