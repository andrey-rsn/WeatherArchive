namespace WeatherArchive.Services.FileConverter
{
    public interface IFileConverter<T> where T : class
    {
        T ConvertFile(FileStream inputFile);
    }
}
