using System.ComponentModel.DataAnnotations;

namespace WeatherArchive.Models
{
    public class WeatherConditions
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public double? AirTemerature { get; set; }

        public double? RelativeHumidity { get; set; }

        public double? Td { get; set; }

        public int? AtmosphericPressure { get; set; }

        public string? WindDirection { get; set; }

        public int? WindSpeed { get; set; }

        public int? CloudCover { get; set; }

        public int? H { get; set; }

        public string? VV { get; set; }

        public string? WeatherPhenomena { get; set; }


    }
}
