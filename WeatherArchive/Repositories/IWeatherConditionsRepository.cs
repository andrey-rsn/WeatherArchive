﻿using WeatherArchive.Models.DTOs;

namespace WeatherArchive.Repositories
{
    public interface IWeatherConditionsRepository
    {
        Task<IEnumerable<WeatherConditionsDTO>> GetWeatherConditionsByYearAndTime(int Year, int Month);

        Task<bool> AddRangeWeatherConditions(IEnumerable<WeatherConditionsDTO> weatherConditionsDTO);

        Task<bool> RemoveWeatherConditions(WeatherConditionsDTO weatherConditions);


    }
}
