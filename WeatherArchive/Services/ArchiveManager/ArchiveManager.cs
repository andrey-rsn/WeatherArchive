using System.Text;
using WeatherArchive.Models;
using WeatherArchive.Models.DTOs;
using WeatherArchive.Repositories;
using WeatherArchive.Services.FileConverter;

namespace WeatherArchive.Services.ArchiveManager
{
    /// <summary>
    /// Archive manager
    /// </summary>
    public class ArchiveManager : IArchiveManager<WeatherConditionsListViewModel>
    {
        private readonly IFileConverter<IEnumerable<WeatherConditionsDTO>> _fileConverter;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IWeatherConditionsRepository _weatherConditionsRepository;
        public ArchiveManager(IFileConverter<IEnumerable<WeatherConditionsDTO>> fileConverter, IWebHostEnvironment appEnvironment, IWeatherConditionsRepository weatherConditionsRepository)
        {
            _fileConverter = fileConverter;
            _appEnvironment = appEnvironment;
            _weatherConditionsRepository = weatherConditionsRepository;
        }

        /// <summary>
        /// Method for creating WeatherConditionsList page ViewModel
        /// </summary>
        /// <param name="Year"> Year </param>
        /// /// <param name="Month"> Month </param>
        /// /// <param name="page"> Page number </param>
        /// /// <param name="pageSize"> Page size </param>
        /// <returns> WeatherConditionsList page ViewModel </returns>
        public async Task<WeatherConditionsListViewModel> GetPageViewModel(int Year, int Month, int page, int pageSize)
        {
            var date = new DateTime(Year, Month, 01);
            var weatherConditionsList = await _weatherConditionsRepository.GetWeatherConditionsByYearAndTime(date.Year, date.Month);

            PageViewModel pageModel = new PageViewModel(weatherConditionsList.Count(), page, pageSize, Year, Month);
            var weatherConditionsPaging = weatherConditionsList.Skip(pageSize * (page - 1)).Take(pageSize);
            var ViewModel = new WeatherConditionsListViewModel()
            {
                WeatherConditions = weatherConditionsPaging,
                PageViewModel = pageModel
            };

            return ViewModel;
        }

        /// <summary>
        /// Method for uploading and save in Db Weather conditions from files
        /// </summary>
        /// <param name="files"> Input files </param>
        /// <returns> bool value </returns>
        /// <exception cref="Exception">
        /// Thrown when the error occurred.
        /// </exception>
        public async Task<string> UploadFiles(IFormFileCollection files)
        {
            StringBuilder ResultMessage = new StringBuilder("Result: ");
            if (files.Count == 0)
            {
                ResultMessage.Append("There are no files uploaded");
                return ResultMessage.ToString();
            }
            foreach (var file in files)
            {
                string filePath=_appEnvironment.WebRootPath + "/Files/" + file.FileName;;
                try
                {
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    using (var fileStream = new FileStream(filePath, FileMode.CreateNew))
                    {
                        await file.CopyToAsync(fileStream);

                    }
                    using (var fileStream = new FileStream(filePath, FileMode.Open))
                    {
                        var weatherConditions = _fileConverter.ConvertFile(fileStream);
                        if (weatherConditions == null)
                        {
                            throw new Exception("File is uncorrect");
                        }
                        var result = await _weatherConditionsRepository.AddRangeWeatherConditions(weatherConditions);
                        if (!result)
                        {
                            throw new Exception("Uploading error");
                        }
                    }
                    System.IO.File.Delete(filePath);
                    ResultMessage.Append("\\r\\n" + "File: " + file.FileName + " -- successfuly uploaded");
                }
                catch (Exception ex)
                {
                    System.IO.File.Delete(filePath);
                    ResultMessage.Append("\\r\\n" + "File: " + file.FileName + " -- " + ex.Message);
                }

            }

            return ResultMessage.ToString();
        }
    }
}
