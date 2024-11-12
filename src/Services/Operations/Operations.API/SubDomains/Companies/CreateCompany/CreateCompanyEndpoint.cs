namespace Operations.API.SubDomains.Companies.CreateCompany;

public record CreateCompanyRequest(Company Company);

public record CreateCompanyResponse(Guid CompanyId);

public class CreateCompanyEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/company", async (CreateCompanyRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateCompanyCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateCompanyResponse>();

            return Results.Created($"/company/{response.CompanyId}", response);
        })
        .WithName("CreateCompany")
        .Produces<CreateCompanyResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Company")
        .WithDescription("Create Company");
    }
}
