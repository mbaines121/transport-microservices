
namespace Operations.API.Persistence;

public class JobRepository : IJobRepository
{
    public Task<Job> CreateJob(Job job, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
