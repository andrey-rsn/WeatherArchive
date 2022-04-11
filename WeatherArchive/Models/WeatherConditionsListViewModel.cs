using WeatherArchive.Models.DTOs;

namespace WeatherArchive.Models
{
    public class WeatherConditionsListViewModel
    {
        public IEnumerable<WeatherConditionsDTO> WeatherConditions { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
