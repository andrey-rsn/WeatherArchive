namespace WeatherArchive.Models.DTOs
{
    public class WeatherConditionsDTO
    {

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public double AirTemerature { get; set; }

        public int RelativeHumidity { get; set; }

        public double Td { get; set; }

        public int AtmosphericPressure { get; set; }

        public string? WindDirection { get; set; }

        public int WindSpeed { get; set; }

        public int CloudCover { get; set; }

        public int H { get; set; }

        public int? VV { get; set; }

        public string? WeatherPhenomena { get; set; }
    }
}
