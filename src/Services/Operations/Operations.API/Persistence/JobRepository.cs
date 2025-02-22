namespace Operations.API.Persistence;

public class JobRepository(IDocumentSession _session, ILogger<JobRepository> _logger) : IJobRepository
{
    public async Task<Guid> CreateJob(Job job, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[Handled to job]");

        _session.Store(job);
        await _session.SaveChangesAsync(cancellationToken);

        return job.JobId;
    }

    public async Task<IEnumerable<Job>> GetJobsAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("[Handled get jobs]");

        var jobs = await _session.Query<Job>().ToListAsync(cancellationToken);

        return jobs;
    }
}
