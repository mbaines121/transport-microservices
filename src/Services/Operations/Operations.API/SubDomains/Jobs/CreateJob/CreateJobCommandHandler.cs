namespace Operations.API.SubDomains.Jobs.CreateJob;

public record CreateJobCommand(Job job) : ICommand<CreateJobResult>;

public record CreateJobResult(Guid id);

public class CreateJobCommandHandler(IJobRepository _jobRepository)
    : ICommandHandler<CreateJobCommand, CreateJobResult>
{
    public async Task<CreateJobResult> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var jobId = await _jobRepository.CreateJob(request.job, cancellationToken);

        return new CreateJobResult(jobId);
    }
}
