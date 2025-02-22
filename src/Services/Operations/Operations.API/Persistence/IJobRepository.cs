
namespace Operations.API.Persistence;

public interface IJobRepository
{
    Task<Guid> CreateJob(Job job, CancellationToken cancellationToken);
    Task<IEnumerable<Job>> GetJobsAsync(CancellationToken cancellationToken);
}
