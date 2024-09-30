namespace Operations.API.Jobs.CreateJob;

public record CreateJobRequest(Job Job);

public record CreateJobResponse(Guid Id);

public class CreateJobEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/job", async (CreateJobRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateJobCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateJobResponse>();

            return Results.Created($"/job/{response.Id}", response);
        })
        .WithName("CreateJob")
        .Produces<CreateJobResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Job")
        .WithDescription("Create Job");
    }
}
