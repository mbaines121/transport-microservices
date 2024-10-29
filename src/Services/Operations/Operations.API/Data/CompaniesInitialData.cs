using Marten.Schema;

namespace Operations.API.Data;

public class CompaniesInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Company>().AnyAsync(cancellation))
        {
            return;
        }

        session.Store(GetPreconfiguredCompanies());

        await session.SaveChangesAsync();
    }

    private static IEnumerable<Company> GetPreconfiguredCompanies() => new List<Company>
    {
        new Company
            {
                CompanyId = Guid.NewGuid(),
                Name = "Eddie Stobart DB",
                Code = "EST DB",
                Address = "1 Eddie Street"
            },
            new Company
            {
                CompanyId = Guid.NewGuid(),
                Name = "CMA CGM DB",
                Code = "CMA DB",
                Address = "1 CMA Street"
            }
    };
}
