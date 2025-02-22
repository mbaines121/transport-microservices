using Operations.API.SubDomains.Jobs.GetJobs.Models;

namespace Operations.API.SubDomains.Companies.GetCompanies;

public record GetJobsQuery() : IQuery<GetJobsResult>;

public record GetJobsResult(IEnumerable<JobViewModel> Jobs);

public class GetJobsQueryHandler(IJobRepository _jobsRepository, ICompanyRepository _companyRepository)
    : IQueryHandler<GetJobsQuery, GetJobsResult>
{
    public async Task<GetJobsResult> Handle(GetJobsQuery query, CancellationToken cancellationToken)
    {
        var jobs = await _jobsRepository.GetJobsAsync(cancellationToken);

        var companies = await _companyRepository.GetCompaniesAsync(cancellationToken);

        var jobViewModels = new List<JobViewModel>();

        foreach (var job in jobs)
        {
            jobViewModels.Add(new JobViewModel
            {
                JobNumber = job.JobNumber,
                //DateCreated = job // TODO
                CustomerName = companies.FirstOrDefault(m => m.CompanyId == job.CustomerId)?.Name ?? "",
                SupplierName = companies.FirstOrDefault(m => m.CompanyId == job.SupplierId)?.Name ?? "",
                ShipperName = companies.FirstOrDefault(m => m.CompanyId == job.ShipperId)?.Name ?? ""
            });
        }

        return new GetJobsResult(jobViewModels);
    }
}