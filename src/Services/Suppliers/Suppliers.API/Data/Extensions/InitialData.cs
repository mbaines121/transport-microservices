namespace Suppliers.API.Data.Extensions;

internal class InitialData
{
    public static IEnumerable<Company> Companies =>
        new List<Company>
        {
            new Company
            {
                CompanyId = Guid.Parse("56536106-EAF8-446D-94D3-F76F50331F44"),
                Name = "Company A",
                Code = "A"
            },
            new Company
            {
                CompanyId = Guid.Parse("5398CE1E-495C-4C1B-A931-5DEED1134914"),
                Name = "Company B",
                Code = "B"
            },
            new Company
            {
                CompanyId = Guid.Parse("B5EC85A0-508C-4C4C-9DF9-5E67DF34302C"),
                Name = "Company C",
                Code = "C"
            }
        };

    public static IEnumerable<Booking> Bookings =>
        new List<Booking>
        {
            new Booking
            {
                BookingId = Guid.NewGuid(),
                CustomerId = Guid.Parse("56536106-EAF8-446D-94D3-F76F50331F44"),
                ShipperId = Guid.Parse("5398CE1E-495C-4C1B-A931-5DEED1134914"),
                SupplierId = Guid.Parse("B5EC85A0-508C-4C4C-9DF9-5E67DF34302C"),
                BookingReference = "MAB00123-DB"
            }
        };
}