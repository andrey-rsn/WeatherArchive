using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using WeatherArchive.Models;
using WeatherArchive.Models.DTOs;
using WeatherArchive.Repositories;
using WeatherArchive.Services.ArchiveManager;
using WeatherArchive.Services.FileConverter;

namespace WeatherArchive.Controllers
{
    
    public class HomeController : Controller
    {

        private readonly IArchiveManager<WeatherConditionsListViewModel> _archiveManager;

        public HomeController(IArchiveManager<WeatherConditionsListViewModel> archiveManager)
        {
            _archiveManager = archiveManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> WeatherConditionsList(int Year=2010,int Month=1,int page=1,int pageSize=5)
        {
            var ViewModel=await _archiveManager.GetPageViewModel(Year, Month, page, pageSize);
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
            var ResultMessage = await _archiveManager.UploadFiles(files);
            ViewBag.Message = ResultMessage;
            return View(nameof(UploadArchive));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}