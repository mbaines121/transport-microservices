using Newtonsoft.Json;

namespace TransportWebUI.Services;

internal interface IBookingService
{
    Task<IEnumerable<Booking>> GetBookingsAsync();
}

public record GetBookingsResponse(IEnumerable<Booking> Bookings);

internal class BookingService : IBookingService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public BookingService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<Booking>> GetBookingsAsync()
    {
        var client = _httpClientFactory.CreateClient("ApiGateway");
        var responseMessage = await client.GetAsync("/suppliers-service/bookings");
        var apiContent = await responseMessage.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<GetBookingsResponse>(apiContent);

        if (response is not null)
        {
            return response.Bookings;
        }

        return new List<Booking>();
    }
}
