namespace Suppliers.API.SubDomains.Companies;

public record GetCompaniesResult(IEnumerable<Company> Companies);

public class CompanyService(IApplicationDbContext _context) : ICompanyService
{
    public async Task<GetCompaniesResult> GetCompaniesAsync()
    {
        var companies = await _context.Companies.ToListAsync();

        return new GetCompaniesResult(companies);
    }
}
