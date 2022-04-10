using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using WeatherArchive.Models;
using WeatherArchive.Models.DTOs;
using WeatherArchive.Repositories;
using WeatherArchive.Services.FileConverter;

namespace WeatherArchive.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFileConverter<IEnumerable<WeatherConditionsDTO>> _fileConverter;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IWeatherConditionsRepository _weatherConditionsRepository;

        
        public HomeController(ILogger<HomeController> logger, IFileConverter<IEnumerable<WeatherConditionsDTO>> fileConverter , IWeatherConditionsRepository weatherConditionsRepository , IWebHostEnvironment appEnvironment )
        {
            _logger = logger;
            _fileConverter = fileConverter;
            _weatherConditionsRepository = weatherConditionsRepository;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> WeatherConditionsList()
        //{
        //    return View();
        //}

        [HttpGet]
        public async Task<IActionResult> WeatherConditionsList(int Year=2010,int Month=1,int page=1)
        {
            var date = new DateTime(Year, Month, 01);
            var weatherConditionsList= await _weatherConditionsRepository.GetWeatherConditionsByYearAndTime(date.Year, date.Month);

            PageViewModel pageModel = new PageViewModel(weatherConditionsList.Count(),page,5);

            var ViewModel = new WeatherConditionsListViewModel()
            {
                WeatherConditions = weatherConditionsList,
                PageViewModel = pageModel
            };

            return View(ViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> UploadArchive()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(IFormFileCollection files)
        {
            StringBuilder ResultMessage=new StringBuilder("");
            if(files.Count==0)
            {
                ResultMessage.Append("There are no files uploaded");
            }
            foreach(var file in files)
            {
                try
                {
                    string filePath = _appEnvironment.WebRootPath + "/Files/" + file.FileName;
                    if(System.IO.File.Exists(filePath))
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
                    ResultMessage.Append("\n " + "File " + file.FileName + ": successfuly uploaded");
                }
                catch (Exception ex)
                {
                    ResultMessage.Append("\n "+"File "+file.FileName+":"+ex.Message);
                }

            }
            
            ViewBag.Message = ResultMessage.ToString();
            return View(nameof(UploadArchive));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}