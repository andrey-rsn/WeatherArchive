namespace WeatherArchive.Services.ArchiveManager
{
    public interface IArchiveManager<T> where T : class
    {
        Task<string> UploadFiles(IFormFileCollection files);
        Task<T> GetPageViewModel(int Year,int Month,int page,int pageSize);
    }
}
