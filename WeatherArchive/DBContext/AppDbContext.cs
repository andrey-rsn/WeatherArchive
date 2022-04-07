using Microsoft.EntityFrameworkCore;

namespace WeatherArchive.DBContext
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {

        }
    }
}
