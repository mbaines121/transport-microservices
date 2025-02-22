using Microsoft.AspNetCore.Mvc;

namespace Suppliers.API.SubDomains.Bookings;

public record GetBookingsResponse(IEnumerable<Booking> bookings);

public class BookingEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/bookings", async ([FromServices] IBookingsService bookingService) =>
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

        app.MapPost("/savebooking", async (BookingViewModel bookingViewModel, IBookingsService bookingService, CancellationToken cancellationToken) =>
        {
            if (bookingViewModel is null)
            {
                return Results.BadRequest();
            }

            await bookingService.SaveBookingAsync(bookingViewModel, cancellationToken);

            return Results.Ok();
        })
        .WithName("Save Booking")
        .Produces<string>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Save Booking")
        .WithDescription("Save Booking");

        app.MapPost("/tojob", async (BookingViewModel bookingViewModel, IBookingsService bookingService, CancellationToken cancellationToken) =>
        {
            if (bookingViewModel is null)
            {
                return Results.BadRequest();
            }

            await bookingService.ToJobAsync(bookingViewModel, cancellationToken);

            return Results.Ok();
        })
        .WithName("To Job")
        .Produces<string>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("To Job")
        .WithDescription("To Job");
    }
}
