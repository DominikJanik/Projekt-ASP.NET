using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using NuGet.Protocol;
using SnowmobileShop.Data;
using SnowmobileShop.Models;
using SnowmobileShop.Services;

namespace SnowmobileShop.Controllers
{
    public class SnowmobileRentalController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICalendarService _calendarService;

        public SnowmobileRentalController(ApplicationDbContext context, ICalendarService calendarService)
        {
            _dbContext = context;
            _calendarService = calendarService;
        }

        public IActionResult Index()
        {
            var snowmobiles = _dbContext.Snowmobiles.ToList();

            return View(snowmobiles);
        }

        public IActionResult Details(int id)
        {
            var snowmobile = _dbContext.Snowmobiles.Include("SnowmobileType").FirstOrDefault(x => x.Id == id);
            return View(snowmobile);
        }


        #region API FOR CALENDAR
        public class DateInfoBody
        {
            public string Date { get; set; }
            public int RentalTimePeriod { get; set; }
            public int ProductId { get; set; }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetDateInfo([FromBody] DateInfoBody body)
        {
            int rentalTimePeriod = body.RentalTimePeriod;
            DateOnly parsedDate;

            if (!DateOnly.TryParse(body.Date, out parsedDate))
            {
                return BadRequest();
            }

            var day = _dbContext.RentalDays.Include("RentalHours").FirstOrDefault(x => x.Product.Id == body.ProductId && x.Date == parsedDate);

            if (day == null)
            {
                var product = _dbContext.Products.FirstOrDefault(x => x.Id == body.ProductId);
                if (product == null) return NotFound();

                day = new RentalDay();
                day.Date = parsedDate;
                day.Product = product;

                _dbContext.RentalDays.Add(day);
                _dbContext.SaveChanges();

                day.RentalHours = _calendarService.GenerateRentalHours(rentalTimePeriod, day);
            }



            return Ok(day);
        }

        #endregion
    }
}
