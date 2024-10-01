namespace Operations.API.Persistence;

public class JobRepository(ILogger<JobRepository> _logger) : IJobRepository
{
    public async Task<Guid> CreateJob(Job job, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[Handled create job]");

        return Guid.NewGuid();
    }
}
