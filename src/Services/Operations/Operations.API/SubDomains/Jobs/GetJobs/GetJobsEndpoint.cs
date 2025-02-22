using Operations.API.SubDomains.Companies.GetCompanies;

namespace Operations.API.SubDomains.Companies.GetJobs;

public record GetJobsResponse(IEnumerable<Job> Jobs);

public class GetJobs : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/jobs", async (ISender sender) =>
        {
            var result = await sender.Send(new GetJobsQuery());

            var response = result.Adapt<GetJobsResponse>();

            return Results.Ok(response);
        })
        .WithName("Get Jobs")
        .Produces<GetCompaniesResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Jobs")
        .WithDescription("Get Jobs");
    }
}