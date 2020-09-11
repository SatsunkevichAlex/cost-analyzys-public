using CostAnalysis.DataSource;
using CostAnalysisAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CostAnalysisAPI.Controllers
{
    [ApiController]
    [Route("Visual")]
    public class VisualController : Controller
    {
        private readonly ApplicationContext _db;

        public VisualController(ApplicationContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("PlotData")]
        public IActionResult PlotData()
        {
            var name = "Days plot";
            var points = _db.Days.Select(it => new DayPoint
            {
                Name = it.Date.ToShortDateString(),
                Value = it.Total
            }).ToList();

            var result = new VisualizationModel
            {
                Name = name,
                Series = points
            };

            return Json(new[] { result });
        }
    }
}