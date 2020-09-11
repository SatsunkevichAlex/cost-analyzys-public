using CostAnalysis.DataSource;
using CostAnalysis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CostAnalysisAPI.Controllers
{
    [ApiController]
    [Route("Days")]
    public class DaysController : Controller
    {
        private readonly ApplicationContext _db;

        public DaysController(ApplicationContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("/")]
        [Route("Index")]
        public IActionResult Index()
        {
            return Ok("Hello from cost analysis");
        }

        [HttpGet]
        [Route("Days")]
        public IActionResult Days(int count = 10)
        {
            return CreatedAtAction(nameof(Days),
                new JsonResult(_db.Days.Take(count)));
        }

        [HttpPost]
        [Route("Days")]
        public async Task<IActionResult> Days([FromBody]Day day)
        {
            string errors = ValidateDay(day);
            if (!string.IsNullOrEmpty(errors))
            {
                return new JsonResult(errors);
            }

            await _db.Days.AddAsync(day);
            await _db.SaveChangesAsync();

            return new JsonResult(day);
        }

        [HttpGet]
        [Route("Day")]
        public IActionResult Day(DateTime date)
        {
            var dayFromDb = _db.Days.SingleOrDefault(d =>
                d.Date.Year == date.Year &&
                d.Date.Month == date.Month &&
                d.Date.Day == date.Day);
            return new JsonResult(dayFromDb);
        }

        [HttpDelete]
        [Route("Day")]
        public async Task<IActionResult> DeleteDay(DateTime date)
        {
            var toRemove = _db.Days.SingleOrDefault(d =>
                d.Date.Year == date.Year &&
                d.Date.Month == date.Month &&
                d.Date.Day == date.Day);
            if (toRemove == null) return new JsonResult("Day not found");

            var removed = _db.Days.Remove(toRemove);

            await _db.SaveChangesAsync();

            return new JsonResult(DayRemoved(removed));
        }


        private string ValidateDay(Day day)
        {
            string errors = string.Empty;
            if (!IsUnique(day))
            {
                errors += "This day already exists in database";
            }
            return errors;
        }

        private bool IsUnique(Day day)
        {
            var daysFromDb = _db.Days.Where(d =>
                d.Date.Year == day.Date.Year &&
                d.Date.Month == day.Date.Month &&
                d.Date.Day == day.Date.Day);
            return daysFromDb.Count() == 0;
        }

        private string DayRemoved(EntityEntry<Day> entry)
        {
            return string.Format("Day {0}-{1}-{2} removed",
                entry.Entity.Date.Year,
                entry.Entity.Date.Month,
                entry.Entity.Date.Day);
        }
    }
}