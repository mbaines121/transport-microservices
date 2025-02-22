using Newtonsoft.Json;

namespace TransportWebUI.Services;

public record GetJobsResponse(IEnumerable<JobViewModel> Jobs);

internal interface IJobService
{
    Task<IEnumerable<JobViewModel>> GetJobsAsync();
}

internal class JobService : IJobService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public JobService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<JobViewModel>> GetJobsAsync()
    {
        var client = _httpClientFactory.CreateClient("ApiGateway");
        var responseMessage = await client.GetAsync("/operations-service/jobs");
        var apiContent = await responseMessage.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<GetJobsResponse>(apiContent);

        var viewModels = new List<JobViewModel>();

        if (response is not null)
        {
            foreach (var job in response.Jobs)
            {
                viewModels.Add(new JobViewModel
                {
                    JobNumber = job.JobNumber,
                    CustomerName = job.CustomerName,
                    SupplierName = job.SupplierName,
                    ShipperName = job.ShipperName,
                    DateCreated = job.DateCreated
                });
            }
        }

        return viewModels;
    }
}
