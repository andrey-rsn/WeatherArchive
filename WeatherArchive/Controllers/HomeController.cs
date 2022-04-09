using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherArchive.Models;
using WeatherArchive.Models.DTOs;
using WeatherArchive.Repositories;
using WeatherArchive.Services.FileConverter;

namespace WeatherArchive.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFileConverter<WeatherConditionsDTO> _fileConverter;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IWeatherConditionsRepository _weatherConditionsRepository;

        public HomeController(ILogger<HomeController> logger, IFileConverter<WeatherConditionsDTO> fileConverter, IWeatherConditionsRepository weatherConditionsRepository = null)
        {
            _logger = logger;
            _fileConverter = fileConverter;
            _weatherConditionsRepository = weatherConditionsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> WeatherConditionsList()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UploadArchive()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(IFormFileCollection files)
        {
            try
            {
                List<WeatherConditionsDTO> weatherConditionsList = new List<WeatherConditionsDTO>();
                foreach(var file in files)
                {

                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath+"/Files/"+file.FileName, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                        var weatherConditions =_fileConverter.ConvertFile(fileStream);
                        weatherConditionsList.Add(weatherConditions);
                    }
                }
                if(weatherConditionsList.Count == 0)
                {
                    throw new ArgumentException("Invalid files.");

                }
                ViewBag.Message = "Файлы успешно загружены.";
                
            }
            catch(Exception ex)
            {
                ViewBag.Message = string.Concat("Ошибка при загрузке файлов:", ex.Message);
            }

            return View("UploadMessage");

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}