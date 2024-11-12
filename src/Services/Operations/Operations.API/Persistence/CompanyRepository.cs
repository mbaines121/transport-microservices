namespace Operations.API.Persistence;

public class CompanyRepository(IDocumentSession _session, ILogger<JobRepository> _logger) : ICompanyRepository
{
    public async Task<Guid> CreateCompanyAsync(Company company, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[Handled create company]");

        _session.Store(company);

        await _session.SaveChangesAsync(cancellationToken);

        return company.CompanyId;
    }

    public async Task<IEnumerable<Company>> GetCompaniesAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("[Handled get companies]");

        var companies = await _session.Query<Company>().ToListAsync(cancellationToken);

        return companies;
    }
}
