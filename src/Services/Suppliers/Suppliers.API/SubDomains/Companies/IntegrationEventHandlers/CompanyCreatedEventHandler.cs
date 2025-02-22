using BuildingBlocks.Messaging.IntegrationEvents.Company;
using Suppliers.API.Data;

namespace Suppliers.API.SubDomains.Companies.IntegrationEventHandlers;

public class CompanyCreatedEventHandler(ILogger<CompanyCreatedEventHandler> _logger, IApplicationDbContext _context) : IConsumer<CompanyCreatedEvent>
{
    public async Task Consume(ConsumeContext<CompanyCreatedEvent> context)
    {
        _logger.LogInformation("[Handled company created integration event]");

        var company = context.Message.Adapt<Company>();

        _context.Companies.Add(company);

        await _context.SaveChangesAsync(context.CancellationToken);
    }
}
