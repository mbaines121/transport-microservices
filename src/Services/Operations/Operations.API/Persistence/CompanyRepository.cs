namespace Operations.API.Persistence;

public class CompanyRepository(IDocumentSession _session, ILogger<JobRepository> _logger) : ICompanyRepository
{
    public async Task<IEnumerable<Company>> GetCompaniesAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("[Handled get companies]");

        var companies = await _session.Query<Company>().ToListAsync(cancellationToken);

        return companies;
    }
}
