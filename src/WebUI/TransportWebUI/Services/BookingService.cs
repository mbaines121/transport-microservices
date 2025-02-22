using BuildingBlocks.Helpers;
using Newtonsoft.Json;
using System.Text;

namespace TransportWebUI.Services;

internal interface IBookingService
{
    Task<IEnumerable<BookingViewModel>> GetBookingsAsync();

    Task SaveBookingAsync(BookingViewModel bookingViewModel);
    Task ToJobAsync(BookingViewModel bookingViewModel);
}

public record GetBookingsResponse(IEnumerable<Booking> Bookings);

internal class BookingService : IBookingService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public BookingService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task ToJobAsync(BookingViewModel bookingViewModel)
    {
        var client = _httpClientFactory.CreateClient("ApiGateway");

        var json = JsonConvert.SerializeObject(bookingViewModel);
        var data = new StringContent(
            content: json,
            encoding: Encoding.UTF8,
            mediaType: "application/json");

        var responseMessage = await client.PostAsync("/suppliers-service/tojob", data);
        var apiContent = await responseMessage.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<string>(apiContent);
    }

    public async Task SaveBookingAsync(BookingViewModel bookingViewModel)
    {
        var client = _httpClientFactory.CreateClient("ApiGateway");

        var json = JsonConvert.SerializeObject(bookingViewModel);
        var data = new StringContent(
            content: json,
            encoding: Encoding.UTF8, 
            mediaType: "application/json");

        var responseMessage = await client.PostAsync("/suppliers-service/savebooking", data);
        var apiContent = await responseMessage.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<string>(apiContent);
    }

    public async Task<IEnumerable<BookingViewModel>> GetBookingsAsync()
    {
        var client = _httpClientFactory.CreateClient("ApiGateway");
        var responseMessage = await client.GetAsync("/suppliers-service/bookings");
        var apiContent = await responseMessage.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<GetBookingsResponse>(apiContent);

        var viewModels = new List<BookingViewModel>();

        if (response is not null)
        {
            foreach (var booking in response.Bookings)
            {
                viewModels.Add(new BookingViewModel
                {
                    BookingId = booking.BookingId,
                    BookingReference = booking.BookingReference,
                    CustomerName = booking?.Customer?.ToListItem() ?? new ListItem(),
                    SupplierName = booking?.Supplier?.ToListItem() ?? new ListItem(),
                    ShipperName = booking?.Shipper?.ToListItem() ?? new ListItem()
                });
            }
        }

        return viewModels;
    }
}
