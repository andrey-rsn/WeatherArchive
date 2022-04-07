using WeatherArchive.Models.DTOs;

namespace WeatherArchive.Repositories
{
    public interface IWeatherConditionsRepository
    {
        Task<IEnumerable<WeatherConditionsDTO>> GetWeatherConditionsByDateAndTime(DateTime Year, DateTime time);

        Task<bool> AddRangeWeatherConditions(IEnumerable<WeatherConditionsDTO> weatherConditionsDTO);

        Task<bool> RemoveWeatherConditions(WeatherConditionsDTO weatherConditions);


    }
}
