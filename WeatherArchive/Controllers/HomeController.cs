using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherArchive.Models;
using WeatherArchive.Services.ArchiveManager;


namespace WeatherArchive.Controllers
{
    /// <summary>
    /// Home controller
    /// </summary>
    public class HomeController : Controller
    {

        private readonly IArchiveManager<WeatherConditionsListViewModel> _archiveManager;

        public HomeController(IArchiveManager<WeatherConditionsListViewModel> archiveManager)
        {
            _archiveManager = archiveManager;
        }

        /// <summary>
        /// Method for getting Index view
        /// </summary>
        /// <returns> Index view </returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        /// <summary>
        /// Method for getting WeatherConditionsList view
        /// </summary>
        /// <returns> WeatherConditionsList view </returns>
        [HttpGet]
        public async Task<IActionResult> WeatherConditionsList(int Year=2010,int Month=1,int page=1,int pageSize=5)
        {
            var ViewModel=await _archiveManager.GetPageViewModel(Year, Month, page, pageSize);
            return View(ViewModel);
        }

        /// <summary>
        /// Method for getting UploadArchive view
        /// </summary>
        /// <returns> UploadArchvie view </returns>
        [HttpGet]
        public async Task<IActionResult> UploadArchive()
        {
            return View();
        }

        /// <summary>
        /// Method for getting UploadFiles view
        /// </summary>
        /// <returns> UploadFiles view </returns>
        [HttpPost]
        public async Task<IActionResult> UploadFiles(IFormFileCollection files)
        {
            var ResultMessage = await _archiveManager.UploadFiles(files);
            ViewBag.Message = ResultMessage;
            return View(nameof(UploadArchive));
        }

        /// <summary>
        /// Method for getting Error view
        /// </summary>
        /// <returns> Error view </returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}