using Mapster;

namespace Suppliers.API.SubDomains.Bookings;

public record GetBookingsResponse(IEnumerable<Booking> bookings);

public class BookingEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/bookings", async (IConfiguration configuration, IBookingsService bookingService) =>
        {
            var bookings = await bookingService.GetBookingsAsync();

            var response = bookings.Adapt<GetBookingsResponse>();

            return Results.Ok(response);
        })
        .WithName("Get Bookings")
        .Produces<string>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Bookings")
        .WithDescription("Get Bookings");
    }
}
