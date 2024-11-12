﻿using BuildingBlocks.Messaging.IntegrationEvents.Company;

namespace Operations.API.SubDomains.Companies.CreateCompany;

public record CreateCompanyCommand(Company Company) : ICommand<CreateCompanyResult>;

public record CreateCompanyResult(Guid CompanyId);

public class CreateCompanyCommandHandler(ICompanyRepository _companyRepository, IPublishEndpoint _publishEndpoint) : ICommandHandler<CreateCompanyCommand, CreateCompanyResult>
{
    public async Task<CreateCompanyResult> Handle(CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        var companyCreatedEvent = command.Company.Adapt<CompanyCreated>();

        await _publishEndpoint.Publish(companyCreatedEvent);

        var companyId = await _companyRepository.CreateCompanyAsync(command.Company, cancellationToken);

        return new CreateCompanyResult(companyId);
    }
}