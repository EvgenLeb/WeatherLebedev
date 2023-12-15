
using Microsoft.AspNetCore.Mvc;
using Weather.Services;
using Weather.Models;
namespace Weather.Controllers
{
    public class WeatherRecordsController : Controller
    {
        private readonly IWeatherRecordService _weatherRecordService;

        public WeatherRecordsController(IWeatherRecordService weatherRecordService)
        {
            _weatherRecordService = weatherRecordService;
        }

        [HttpGet]
        public IActionResult Index(string? city)
        {
            IEnumerable<WeatherRecord> weatherRecords;

            if (city != null)
            {
                weatherRecords = _weatherRecordService.SearchByCity(city);
            }
            else
            {
                weatherRecords = _weatherRecordService.GetAll();
            }

            return View(weatherRecords);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Действие для обработки данных формы
        [HttpPost]
        public IActionResult Create(WeatherRecord weatherRecord)
        {
            if (ModelState.IsValid)
            {
                _weatherRecordService.Add(weatherRecord);
                return RedirectToAction("Index");
            }

            return View(weatherRecord);
        }
    }
}
