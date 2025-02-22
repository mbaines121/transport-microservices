using BuildingBlocks.Messaging.IntegrationEvents.Booking;

namespace Operations.API.SubDomains.Jobs.IntegrationEventHandlers;

public class BookingToJobEventHandler(ILogger<BookingToJobEventHandler> _logger, IJobRepository _jobRepository) : IConsumer<BookingToJobEvent>
{
    public async Task Consume(ConsumeContext<BookingToJobEvent> context)
    {
        _logger.LogInformation("[Handled company created integration event]");

        var job = new Job
        {
            JobId = Guid.NewGuid(),
            BookingId = context.Message.BookingId,
            CustomerId = context.Message.CustomerId,
            SupplierId = context.Message.SupplierId,
            ShipperId = context.Message.ShipperId,
            JobNumber = "jid-" + context.Message.BookingReference
        };

        var jobId = await _jobRepository.CreateJob(job, context.CancellationToken);
    }
}