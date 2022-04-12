using Microsoft.EntityFrameworkCore;
using WeatherArchive.Models;

namespace WeatherArchive.DBContext
{
    /// <summary>
    /// MSsql database context
    /// </summary>
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {

        }

        public DbSet<WeatherConditions> weatherConditions { get; set; }
    }
}
