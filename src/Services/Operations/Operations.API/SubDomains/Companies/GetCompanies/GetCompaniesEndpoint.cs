using Operations.API.SubDomains.Companies.GetCompanies;

namespace Operations.API.SubDomains.Companies.GetJobs;

public record GetCompaniesResponse(IEnumerable<Company> Companies);

public class GetCompanies : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/companies", async (ISender sender) =>
        {
            var result = await sender.Send(new GetCompaniesQuery());

            var response = result.Adapt<GetCompaniesResponse>();

            return Results.Ok(response);
        })
        .WithName("GetCompanies")
        .Produces<GetCompaniesResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Companies")
        .WithDescription("Get Companies");
    }
}