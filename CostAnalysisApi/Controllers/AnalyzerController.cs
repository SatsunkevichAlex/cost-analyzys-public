using CostAnalysisAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CostAnalysisAPI.Controllers
{
    [ApiController]
    [Route("days-analyzer")]
    public class AnalyzerController : Controller
    {
        private readonly DaysAnalyzerService _analyzer;

        public AnalyzerController(DaysAnalyzerService analyzer)
        {
            _analyzer = analyzer;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return Json("Hello from days analyzer");
        }

        [HttpGet]
        [Route("max-day")]
        public IActionResult GetMaxDay()
        {
            return Json(_analyzer.GetMaxDay());
        }

        [HttpGet]
        [Route("day-by-date")]
        public IActionResult GetDayByDate(string date)
        {
            var result = _analyzer.GetDayByDate(date);
            return Json(result);
        }

        [HttpGet]
        [Route("total-days-count")]
        public IActionResult GetTotalDaysCount()
        {
            return Json(_analyzer.GetTotalDaysCount());
        }

        [HttpGet]
        [Route("days-total-average")]
        public IActionResult GetDaysTotalAverage()
        {
            return Json(_analyzer.GetDaysTotalAverage());
        }
    }
}