using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WeatherArchive.DBContext;
using WeatherArchive.Models;
using WeatherArchive.Models.DTOs;

namespace WeatherArchive.Repositories
{
    public class WeatherConditionsRepository : IWeatherConditionsRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public WeatherConditionsRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<bool> AddRangeWeatherConditions(IEnumerable<WeatherConditionsDTO> weatherConditionsDTO)
        {
            if(weatherConditionsDTO == null)
            {
                return false;
            }
            var weatherConditions = _mapper.Map<List<WeatherConditions>>(weatherConditionsDTO);
            await _dbContext.weatherConditions.AddRangeAsync(weatherConditions);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<WeatherConditionsDTO>> GetWeatherConditionsByYearAndTime(int Year, int Month)
        {

            var WeatherConditions = await _dbContext.weatherConditions.Where(x => x.Date.Year == Year && x.Date.Month == Month).ToListAsync();
            if(WeatherConditions == null)
            {
                return new List<WeatherConditionsDTO>();
            }
            var result = _mapper.Map<IEnumerable<WeatherConditionsDTO>>(WeatherConditions);
            return result;
        }

        public async Task<bool> RemoveWeatherConditions(WeatherConditionsDTO weatherConditionsDTO)
        {
            try
            {
                var WeatherConditionsModel=_mapper.Map<WeatherConditions>(weatherConditionsDTO);
                WeatherConditions weatherConditions = await _dbContext.weatherConditions.FindAsync(WeatherConditionsModel);
                if(weatherConditions == null)
                { 
                    return false; 
                }
                _dbContext.weatherConditions.Remove(weatherConditions);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
