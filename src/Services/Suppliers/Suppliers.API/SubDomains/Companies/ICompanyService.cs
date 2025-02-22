namespace Suppliers.API.SubDomains.Companies;

public interface ICompanyService
{
    Task<GetCompaniesResult> GetCompaniesAsync();
}
