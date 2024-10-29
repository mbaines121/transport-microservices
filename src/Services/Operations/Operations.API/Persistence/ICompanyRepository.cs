namespace Operations.API.Persistence;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetCompaniesAsync(CancellationToken cancellationToken);
}
