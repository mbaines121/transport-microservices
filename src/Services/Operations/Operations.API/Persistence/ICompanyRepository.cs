namespace Operations.API.Persistence;

public interface ICompanyRepository
{
    Task<Guid> CreateCompanyAsync(Company company, CancellationToken cancellationToken);
    Task<IEnumerable<Company>> GetCompaniesAsync(CancellationToken cancellationToken);
}
