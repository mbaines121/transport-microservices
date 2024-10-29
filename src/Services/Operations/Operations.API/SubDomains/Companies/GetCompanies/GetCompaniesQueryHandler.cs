namespace Operations.API.SubDomains.Companies.GetCompanies;

public record GetCompaniesQuery() : IQuery<GetCompaniesResult>;

public record GetCompaniesResult(IEnumerable<Company> Companies);

public class GetCompaniesQueryHandler(ICompanyRepository _companiesRepository)
    : IQueryHandler<GetCompaniesQuery, GetCompaniesResult>
{
    public async Task<GetCompaniesResult> Handle(GetCompaniesQuery query, CancellationToken cancellationToken)
    {
        var companies = await _companiesRepository.GetCompaniesAsync(cancellationToken);

        return new GetCompaniesResult(companies);
    }
}