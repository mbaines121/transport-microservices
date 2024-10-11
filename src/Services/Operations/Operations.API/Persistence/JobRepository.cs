namespace Operations.API.Persistence;

public class JobRepository(IDocumentSession _session, ILogger<JobRepository> _logger) : IJobRepository
{
    public async Task<Guid> CreateJob(Job job, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[Handled create job]");

        _session.Store(job);
        await _session.SaveChangesAsync(cancellationToken);

        return job.JobId;
    }
}
