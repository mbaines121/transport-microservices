using Newtonsoft.Json;

namespace TransportWebUI.Services;

internal interface ICompanyService
{
    Task<IEnumerable<Company>> GetCompaniesAsync();
}

public record GetCompaniesResponse(IEnumerable<Company> Companies);

internal class CompanyService : ICompanyService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CompanyService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<Company>> GetCompaniesAsync()
    {
        var client = _httpClientFactory.CreateClient("ApiGateway");
        var responseMessage = await client.GetAsync("/operations-service/companies");
        var apiContent = await responseMessage.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<GetCompaniesResponse>(apiContent);

        if (response is not null)
        {
            return response.Companies;
        }

        return new List<Company>();
    }
}
