namespace WeatherArchive.Managers
{
    public interface IWeatherArchiveManager
    {
        Task<string> UlpoadArchiveInDb(IFormFileCollection files);


    }
}
