namespace CMS.Domain.ValueObjects
{
    public record Location
    {
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public string Address { get; init; }
        public string City { get; init; }
        public string Country { get; init; }

        private Location(double latitude,
            double longitude,
            string address,
            string city,
            string country)
        {
            Latitude = latitude;
            Longitude = longitude;
            Address = address;
            City = city;
            Country = country;
        }

        public static Location Of(double latitude, double longitude, string address, string city, string country)
        {
            return new Location(latitude, longitude, address, city, country);
        }

        public override string ToString() => $"{Address}, {City}, {Country} ({Latitude},{Longitude})";

    }
}
