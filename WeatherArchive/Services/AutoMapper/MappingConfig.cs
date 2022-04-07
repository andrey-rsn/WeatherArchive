using AutoMapper;
using WeatherArchive.Models;
using WeatherArchive.Models.DTOs;

namespace WeatherArchive.Services.AutoMapper
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<WeatherConditions, WeatherConditionsDTO>().ReverseMap();
                config.CreateMap<IEnumerable<WeatherConditions>, IEnumerable<WeatherConditionsDTO>>().ReverseMap();

            });
            return mappingConfig;
        }
    }
}
