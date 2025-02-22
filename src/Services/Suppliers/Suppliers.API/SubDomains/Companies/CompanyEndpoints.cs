using Suppliers.API.SubDomains.Companies;

namespace Suppliers.API.SubDomains.Bookings;

public record GetCompaniesResponse(IEnumerable<Company> Companies);

public class CompanyEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/companies", async (IConfiguration configuration, ICompanyService companyService) =>
        {
            var companies = await companyService.GetCompaniesAsync();

            var response = companies.Adapt<GetCompaniesResponse>();

            return Results.Ok(response);
        })
        .WithName("Get Companies")
        .Produces<string>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Companies")
        .WithDescription("Get Companies");
    }
}
