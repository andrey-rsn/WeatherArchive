using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WeatherArchive.DBContext;
using WeatherArchive.Models;
using WeatherArchive.Models.DTOs;

namespace WeatherArchive.Repositories
{
    /// <summary>
    /// WeatherConditions repository
    /// </summary>
    public class WeatherConditionsRepository : IWeatherConditionsRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public WeatherConditionsRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Method for adding Weather conditions in Db
        /// </summary>
        /// <param name="weatherConditionsDTO"> Data to save </param>
        /// <returns> bool value </returns>
        /// <exception cref="Exception">
        /// Thrown when the error occurred.
        /// </exception>
        public async Task<bool> AddRangeWeatherConditions(IEnumerable<WeatherConditionsDTO> weatherConditionsDTO)
        {
            if(weatherConditionsDTO == null)
            {
                return false;
            }

            try
            {
                var weatherConditions = _mapper.Map<List<WeatherConditions>>(weatherConditionsDTO);
                await _dbContext.weatherConditions.AddRangeAsync(weatherConditions);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }

        /// <summary>
        /// Method for getting data from Db by Year an Month
        /// </summary>
        /// <param name="Year"> Year </param>
        /// <param name="Month"> Month </param>
        /// <returns> Collection of WeatherConditionsDTO models </returns>
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

        /// <summary>
        /// Method for remove data from Db
        /// </summary>
        /// <param name="weatherConditionsDTO"> Data to remove </param>
        /// <returns> bool value </returns>
        /// <exception cref="Exception">
        /// Thrown when the error occurred.
        /// </exception>
        public async Task<bool> RemoveWeatherConditions(WeatherConditionsDTO weatherConditionsDTO)
        {
            try
            {
                if(weatherConditionsDTO==null)
                {
                    return false;
                }
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
